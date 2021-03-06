using NProf.Glue.Profiler.Project;

using ICSharpCode.SharpZipLib.Zip;

using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace NProf.Utilities.DataStore
{
	public class SerializationHandler
	{
		public static string DataStoreDirectory;

		private static Hashtable _projectInfoToFileNameMap = Hashtable.Synchronized( new Hashtable() );

		#region Properties

		private static string ProjectsHistoryPath
		{
			get
			{
				string path = DataStoreDirectory;
				Directory.CreateDirectory( path );

				return path + "\\Projects.xml";
			}
		}

		public static UsedFile[] ProjectsHistory
		{
			get
			{
				try
				{
					if( File.Exists( ProjectsHistoryPath ) )
					{
						FileStream inputFile = new FileStream( ProjectsHistoryPath, FileMode.Open );
						XmlSerializer s = new XmlSerializer( typeof( UsedFile[] ) );

						UsedFile[] usedFiles = ( UsedFile[] )s.Deserialize( inputFile );

						inputFile.Close();

						return usedFiles;
					}
				}
				catch ( Exception )
				{
					// Ignore errors for now.
				}

				return new UsedFile[0];
			}
		}

		private static UsedFile[] InternalProjectsHistory
		{
			set
			{
				FileStream outputFile = new FileStream( ProjectsHistoryPath, FileMode.Create );
				XmlSerializer s = new XmlSerializer( typeof( UsedFile[] ) );

				Array.Sort( value );
				Array.Reverse( value );

				s.Serialize( outputFile, value );

				outputFile.Close();
			}
		}

		#endregion

		static SerializationHandler() { }

		/// <summary>
		/// Opens a ProjectInfo from the specified file location
		/// </summary>
		/// <param name="fileName">The absolute path to the file</param>
		/// <returns>An instance of ProjectInfo if the file is valid, null if otherwise</returns>
		public static ProjectInfo OpenProjectInfo( string fileName )
		{
			if( !File.Exists( fileName ) )
				return null;

			// open zip file and get a stream to the ProjectInfo file
			ZipFile zipFile = new ZipFile( fileName );
			ProjectInfo info = null;

			ZipEntry entry = zipFile.GetEntry( "ProjectInfo.xml" );
			Stream inputStream = zipFile.GetInputStream( entry );
			// deserialize that into a new ProjectInfo
			XmlSerializer s = new XmlSerializer( typeof( ProjectInfo ) );
			info = ( ProjectInfo )s.Deserialize( inputStream );

			int nIndex = 0;

			ZipEntry zeRun;
			XmlSerializer xsRun = new XmlSerializer( typeof( Run ) );

			while ( ( zeRun = zipFile.GetEntry( String.Format( "Run-{0:00000000}.xml", nIndex ) ) ) != null )
			{
				Run r = ( Run )xsRun.Deserialize( zipFile.GetInputStream( zeRun ) );
				r.Project = info;
				info.Runs.Add( r );

				nIndex++;
			}

			zipFile.Close();

			// add this for looking up later
			_projectInfoToFileNameMap[ info ] = fileName;

			// make the file recently used and return
			MakeRecentlyUsed( info, fileName );
			return info;
		}

		/// <summary>
		/// Saves a ProjectInfo into the file it was originally opened in, to save a new ProjectInfo use the other overload
		/// </summary>
		/// <param name="info">The previously opened ProjectInfo</param>
		public static void SaveProjectInfo( ProjectInfo info )
		{
			if( _projectInfoToFileNameMap.Contains( info ) ) // one that has already been opened
			{
				string fileName = ( string )_projectInfoToFileNameMap[ info ];

				SaveProjectInfo( info, fileName );
			}
		}

		/// <summary>
		/// Saves a ProjectInfo into the specified filename
		/// </summary>
		/// <param name="info">The ProjectInfo to save</param>
		/// <param name="fileName">The file to save it in</param>
		public static void SaveProjectInfo( ProjectInfo info, string fileName )
		{
			ZipOutputStream zos = new ZipOutputStream( File.Create( fileName ) );
			zos.SetLevel( 7 );

			// Set the file format version
			FileFormat ff = new FileFormat();
			ff.Version = 0;
			ff.Program = "NProf v0.8";

			AddFileToZip( zos, "NProfFileFormat.xml" );
			XmlSerializer sff = new XmlSerializer( typeof( FileFormat ) );
			sff.Serialize( zos, ff );

			// put that buffer as a file into a zip file
			AddFileToZip( zos, "ProjectInfo.xml" );
			XmlSerializer s = new XmlSerializer( typeof( ProjectInfo ) );
			s.Serialize( zos, info );

			XmlSerializer xsRun = new XmlSerializer( typeof( Run ) );

			int nIndex = 0;

			foreach ( Run r in info.Runs )
			{
				string strFilename = String.Format( "Run-{0:00000000}.xml", nIndex );
				AddFileToZip( zos, strFilename );
				xsRun.Serialize( zos, r );

				nIndex++;
			}

			zos.Close();

			// remember for later
			_projectInfoToFileNameMap[ info ] = fileName;

			// make it recently used
			MakeRecentlyUsed( info, fileName );
		}

		/// <summary>
		/// Will return the filename of a ProjectInfo
		/// </summary>
		/// <param name="info">The ProjectInfo to lookup the filename for</param>
		/// <returns>The filename of the ProjectInfo or empty string if not known</returns>
		public static string GetFilename( ProjectInfo info )
		{
			return ( ( string )_projectInfoToFileNameMap[ info ] ) + string.Empty;
		}

		#region Helper functions
		private static void AddFileToZip( ZipOutputStream outputStream, string fileName )
		{
			ZipEntry entry = new ZipEntry( fileName );

			entry.CompressionMethod = CompressionMethod.Deflated;
			entry.DateTime = DateTime.Now;

			outputStream.PutNextEntry(entry);
		}

		private static void MakeRecentlyUsed( ProjectInfo project, string fileName )
		{
			UsedFile[] usedFiles = ProjectsHistory;

			bool found = false;
			foreach( UsedFile usedFile in usedFiles )
			{
				if( usedFile.FileName == fileName )
				{
					usedFile.LastUsed = DateTime.Now;
					usedFile.ProjectName = project.Name;
					found = true;
					break;
				}
			}

			if( !found )
			{
				UsedFile[] temp = usedFiles;
				usedFiles = new UsedFile[ usedFiles.Length + 1];
				Array.Copy( temp, usedFiles, temp.Length );

				UsedFile uf = new UsedFile( fileName, project.Name, DateTime.Now );

				usedFiles[ temp.Length ] = uf;
			}

			InternalProjectsHistory = usedFiles;
		}
		#endregion
	}
}
