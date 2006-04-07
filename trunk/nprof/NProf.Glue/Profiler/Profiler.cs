using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.ServiceProcess;
using NProf.Glue.Profiler.Core;
using NProf.Glue.Profiler.Info;
using NProf.Glue.Profiler.Project;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace NProf.Glue.Profiler
{
	/// <summary>
	/// Summary description for Profiler.
	/// </summary>
	[Serializable]
	public class Profiler
	{
		private const string PROFILER_GUID = "{791DA9FE-05A0-495E-94BF-9AD875C4DF0F}";
		public Profiler()
		{
			_htFunctionMap = new Hashtable();
		}

		public static string Version
		{
			get { return "0.9-alpha"; }
		}

		public bool CheckSetup( out string strMessage )
		{
			strMessage = String.Empty;
			using ( RegistryKey rk = Registry.ClassesRoot.OpenSubKey( "CLSID\\" + PROFILER_GUID ) )
			{
				if ( rk == null )
				{
					strMessage = "Unable to find the registry key for the profiler hook.  Please register the NProf.Hook.dll file.";
					return false;
				}
			}

			return true;
		}

		public bool Start( ProjectInfo pi, Run run, ProcessCompletedHandler pch )
		{
			_dtStart = DateTime.Now;
			_pi = pi;
			_pch = pch;
			_run = run;
			_run.State = Run.RunState.Initializing;

			_pss = new ProfilerSocketServer( pi.Options, run );
			_pss.Start();
			_pss.Exited += new EventHandler( OnProcessExited );
			_pss.Error += new ProfilerSocketServer.ErrorHandler( OnError );
			_pss.Message += new ProfilerSocketServer.MessageHandler( OnMessage );

			switch ( pi.ProjectType )
			{
				case ProjectType.File:
				{
					_p = new Process();
					_p.StartInfo = new ProcessStartInfo( pi.ApplicationName, pi.Arguments );
					_p.StartInfo.EnvironmentVariables[ "COR_ENABLE_PROFILING" ] = "0x1";
					_p.StartInfo.EnvironmentVariables[ "COR_PROFILER" ] = PROFILER_GUID;
					_p.StartInfo.EnvironmentVariables[ "NPROF_PROFILING_SOCKET" ] = _pss.Port.ToString();
					_p.StartInfo.UseShellExecute = false;
					_p.StartInfo.Arguments = pi.Arguments;
					_p.StartInfo.WorkingDirectory = pi.WorkingDirectory;
					_p.EnableRaisingEvents = true;
					//_p.Exited += new EventHandler( OnProcessExited );

					return _p.Start();
				}

				case ProjectType.AspNet:
				{
					using ( RegistryKey rk = Registry.LocalMachine.OpenSubKey( @"SYSTEM\CurrentControlSet\Services\W3SVC", true ) )
					{
						if ( rk != null )
							SetRegistryKeys( rk, true );
					}

					using ( RegistryKey rk = Registry.LocalMachine.OpenSubKey( @"SYSTEM\CurrentControlSet\Services\IISADMIN", true ) )
					{
						if ( rk != null )
							SetRegistryKeys( rk, true );
					}

					Process p = Process.Start( "iisreset.exe", "" );
					p.WaitForExit();
					_run.Messages.AddMessage( "Navigate to your project and ASP.NET will connect to the profiler" );
					_run.Messages.AddMessage( "NOTE: ASP.NET must be set to run under the SYSTEM account in machine.config" );
					_run.Messages.AddMessage( @"If ASP.NET cannot be profiled, ensure that the userName=""SYSTEM"" in the <processModel> section of machine.config." );

					return true;
				}

				case ProjectType.VSNet:
				{
					SetEnvironmentVariable( "COR_ENABLE_PROFILING", "0x1" );
					SetEnvironmentVariable( "COR_PROFILER", PROFILER_GUID );
					SetEnvironmentVariable( "NPROF_PROFILING_SOCKET", _pss.Port.ToString() );

					return true;
				}

				default:
					throw new InvalidOperationException( "Unknown project type: " + pi.ProjectType );
			}
		}

		public void Disable()
		{
			SetEnvironmentVariable( "COR_ENABLE_PROFILING", "0x0" );
		}

		public void Stop()
		{
			Run run;

			lock ( _oRunLock )
			{
				run = _run;

				// Is there anything to stop?
				if ( _run == null )
					return;

				_run = null;
			}

			// Stop the profiler socket server if profilee hasn't connected
			if ( run.State == Run.RunState.Initializing )
			{
				run.Messages.AddMessage( "Shutting down profiler..." );
				_pss.Stop();
				run.State = Run.RunState.Finished;
				run.Success = false;
			}

			if ( _pi.ProjectType == ProjectType.AspNet )
			{
				using ( RegistryKey rk = Registry.LocalMachine.OpenSubKey( @"SYSTEM\CurrentControlSet\Services\W3SVC", true ) )
				{
					if ( rk != null )
						SetRegistryKeys( rk, false );
				}

				using ( RegistryKey rk = Registry.LocalMachine.OpenSubKey( @"SYSTEM\CurrentControlSet\Services\IISADMIN", true ) )
				{
					if ( rk != null )
						SetRegistryKeys( rk, false );
				}

				run.Messages.AddMessage( "Terminating ASP.NET..." );
				Process.Start( "iisreset.exe", "/stop" ).WaitForExit();
			}
		}

		private void OnProcessExited( object oSender, EventArgs ea )
		{
			Run run;

			lock ( _oRunLock )
			{
				run = _run;

				// This gets called twice for file projects - FIXME
				if ( _run == null )
					return;

				_run = null;
			}

			if ( !_pss.HasStoppedGracefully )
			{
				if ( run.State == Run.RunState.Initializing )
				{
					run.Messages.AddMessage( "No connection made with profiled application." );
					run.Messages.AddMessage( "Application might not support .NET." );
				}
				else
				{
					run.Messages.AddMessage( "Application stopped before profiler information could be retrieved." );
				}

				run.Success = false;
				run.State = Run.RunState.Finished;
				run.Messages.AddMessage( "Profiler run did not complete successfully." );
			}
			else
			{
				run.Success = true;
			}

			_dtEnd = DateTime.Now;
			run.Messages.AddMessage( "Stopping profiler listener..." );
			_pss.Stop();
//			if ( ProcessCompleted != null )
//				ProcessCompleted( _pss.ThreadInfoCollection );

			run.EndTime = _dtEnd;

			_pch( run );
		}

		private void OnError( Exception e )
		{
			if ( Error != null )
				Error( e );
		}

		private void OnMessage( string strMessage )
		{
			if ( Message != null )
				Message( strMessage );
		}

		public int[] GetFunctionIDs()
		{
			return ( int[] )new ArrayList( _htFunctionMap.Keys ).ToArray( typeof( int ) );
		}

		public string GetFunctionSignature( int nFunctionID )
		{
			return ( string )_htFunctionMap[ nFunctionID ];
		}

		private void SetRegistryKeys( RegistryKey rk, bool bSet )
		{
			if ( rk == null )
				return;
			
			if ( !bSet )
			{
				// Get rid of the environment
				rk.DeleteValue( "Environment", false );
				return;
			}

			object oKeys = rk.GetValue( "Environment" );
			
			// If it's not something we expected, fix it
			if ( oKeys == null || !( oKeys is string[] ) )
				oKeys = new string[ 0 ]; 

			// Save the environment the first time through
			if ( rk.GetValue( "nprof Saved Environment" ) == null && ( ( string[] )oKeys ).Length > 0 )
				rk.SetValue( "nprof Saved Environment", oKeys );

			Hashtable htItems = new Hashtable( Environment.GetEnvironmentVariables() );

			// Set the environment to be the default system environment
			using ( RegistryKey rkEnv = Registry.LocalMachine.OpenSubKey( @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment" ) )
			{
				if ( rkEnv == null )
					throw new InvalidOperationException( "Unable to locate machine environment key" );

				foreach ( string strValueName in rkEnv.GetValueNames() )
					htItems[ strValueName ] = rkEnv.GetValue( strValueName );
				
			}

			htItems.Remove( "COR_ENABLE_PROFILING" );
			htItems.Remove( "COR_PROFILER" );
			htItems.Remove( "NPROF_PROFILING_SOCKET" );

			htItems.Add( "COR_ENABLE_PROFILING", "0x1" );
			htItems.Add( "COR_PROFILER", PROFILER_GUID );
			htItems.Add( "NPROF_PROFILING_SOCKET", _pss.Port.ToString() );

			ArrayList alItems = new ArrayList();
			foreach ( DictionaryEntry de in htItems )
				alItems.Add( String.Format( "{0}={1}", de.Key, de.Value ) );

			rk.SetValue( "Environment", ( string[] )alItems.ToArray( typeof( string ) ) );
		}

		public delegate void ProcessCompletedHandler( Run run );
		[field:NonSerialized]
		public event ProcessCompletedHandler ProcessCompleted;
		public delegate void ErrorHandler( Exception e );
		[field:NonSerialized]
		public event ErrorHandler Error;
		public delegate void MessageHandler( string strMessage );
		[field:NonSerialized]
		public event MessageHandler Message;

		[NonSerialized]
		private ProcessCompletedHandler _pch;
		private DateTime _dtStart;
		private DateTime _dtEnd;
		private Run _run;
		private object _oRunLock = 0;

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto )]
		private static extern bool SetEnvironmentVariable( string strVariable, string strNewValue );

		private Hashtable _htFunctionMap;
		[NonSerialized]
		private Process _p;
		private ProjectInfo _pi;
		[NonSerialized]
		private ProfilerSocketServer _pss;
	}
}
