using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NProf.Glue.Profiler;
using NProf.Glue.Profiler.Info;
using NProf.Glue.Profiler.Project;
using Crownwood.Magic.Menus;
using Genghis.Windows.Forms;
using DotNetLib.Windows.Forms;

namespace NProf.GUI
{
	/// <summary>
	/// Summary description for ProfilerControl.
	/// </summary>
	[ProgId( "NProf.ProfilerControl" ), 
	ClassInterface( ClassInterfaceType.AutoDual ),
	Guid( "D34B8507-C286-4d40-83BC-0852E19DEC89" )]
	public class ProfilerControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TreeView _tvNamespaceInfo;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionID;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionSignature;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionCalls;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionTotal;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionMethod;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionChildren;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colFunctionSuspended;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCalleeID;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCalleeSignature;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCalleeCalls;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCalleeTotal;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCalleeParent;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCallerID;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCallerSignature;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCallerCalls;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCallerTotal;
		private DotNetLib.Windows.Forms.ToggleColumnHeader colCallerParent;
		private System.Windows.Forms.Timer _tmrFilterThrottle;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label _lblFilterSignatures;
		private System.Windows.Forms.TextBox _txtFilterBar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _cbCurrentThread;
		private System.Windows.Forms.Panel panel3;
		private DotNetLib.Windows.Forms.ContainerListView _lvFunctionInfo;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.TabControl _tcCalls;
		private System.Windows.Forms.TabPage _tpCallees;
		private DotNetLib.Windows.Forms.ContainerListView _lvCalleesInfo;
		private System.Windows.Forms.TabPage _tcCallers;
		private DotNetLib.Windows.Forms.ContainerListView _lvCallersInfo;
		private System.ComponentModel.IContainer components;

		public ProfilerControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			_htCheckedItems = new Hashtable();
			_bUpdating = false;
			_bInCheck = false;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this._lvFunctionInfo = new DotNetLib.Windows.Forms.ContainerListView();
			this.colFunctionID = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colFunctionSignature = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colFunctionCalls = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colFunctionTotal = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colFunctionMethod = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colFunctionChildren = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colFunctionSuspended = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this._tcCalls = new System.Windows.Forms.TabControl();
			this._tpCallees = new System.Windows.Forms.TabPage();
			this._lvCalleesInfo = new DotNetLib.Windows.Forms.ContainerListView();
			this.colCalleeID = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCalleeSignature = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCalleeCalls = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCalleeTotal = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCalleeParent = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this._tcCallers = new System.Windows.Forms.TabPage();
			this._lvCallersInfo = new DotNetLib.Windows.Forms.ContainerListView();
			this.colCallerID = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCallerSignature = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCallerCalls = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCallerTotal = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.colCallerParent = new DotNetLib.Windows.Forms.ToggleColumnHeader();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this._tvNamespaceInfo = new System.Windows.Forms.TreeView();
			this.panel2 = new System.Windows.Forms.Panel();
			this._lblFilterSignatures = new System.Windows.Forms.Label();
			this._txtFilterBar = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._cbCurrentThread = new System.Windows.Forms.ComboBox();
			this._tmrFilterThrottle = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this._tcCalls.SuspendLayout();
			this._tpCallees.SuspendLayout();
			this._tcCallers.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this._tvNamespaceInfo);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(872, 456);
			this.panel1.TabIndex = 10;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this._lvFunctionInfo);
			this.panel3.Controls.Add(this.splitter2);
			this.panel3.Controls.Add(this._tcCalls);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(195, 24);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(677, 432);
			this.panel3.TabIndex = 13;
			// 
			// _lvFunctionInfo
			// 
			this._lvFunctionInfo.AllowColumnReorder = true;
			this._lvFunctionInfo.AllowMultiSelect = true;
			this._lvFunctionInfo.CaptureFocusClick = false;
			this._lvFunctionInfo.Columns.AddRange(new DotNetLib.Windows.Forms.ToggleColumnHeader[] {
																																	this.colFunctionID,
																																	this.colFunctionSignature,
																																	this.colFunctionCalls,
																																	this.colFunctionTotal,
																																	this.colFunctionMethod,
																																	this.colFunctionChildren,
																																	this.colFunctionSuspended});
			this._lvFunctionInfo.ColumnSortColor = System.Drawing.Color.WhiteSmoke;
			this._lvFunctionInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this._lvFunctionInfo.HeaderHeight = 33;
			this._lvFunctionInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
			this._lvFunctionInfo.HideSelection = false;
			this._lvFunctionInfo.Location = new System.Drawing.Point(0, 0);
			this._lvFunctionInfo.MultipleColumnSort = true;
			this._lvFunctionInfo.Name = "_lvFunctionInfo";
			this._lvFunctionInfo.Size = new System.Drawing.Size(677, 253);
			this._lvFunctionInfo.TabIndex = 24;
			this._lvFunctionInfo.SelectedItemsChanged += new System.EventHandler(this._lvFunctionInfo_SelectedItemsChanged);
			// 
			// colFunctionID
			// 
			this.colFunctionID.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Integer;
			this.colFunctionID.Text = "ID";
			this.colFunctionID.ToolTip = "ID Tool Tip";
			this.colFunctionID.Width = 100;
			// 
			// colFunctionSignature
			// 
			this.colFunctionSignature.DisplayIndex = 1;
			this.colFunctionSignature.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.String;
			this.colFunctionSignature.Text = "Signature";
			this.colFunctionSignature.ToolTip = "Signature Tool Tip";
			this.colFunctionSignature.Width = 350;
			// 
			// colFunctionCalls
			// 
			this.colFunctionCalls.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colFunctionCalls.DisplayIndex = 2;
			this.colFunctionCalls.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colFunctionCalls.Text = "# of Calls";
			this.colFunctionCalls.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colFunctionCalls.ToolTip = "# of Calls Tool Tip";
			this.colFunctionCalls.Width = 70;
			// 
			// colFunctionTotal
			// 
			this.colFunctionTotal.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colFunctionTotal.DisplayIndex = 3;
			this.colFunctionTotal.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colFunctionTotal.Text = "% of Total";
			this.colFunctionTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colFunctionTotal.ToolTip = "% of Total Tool Tip";
			this.colFunctionTotal.Width = 70;
			// 
			// colFunctionMethod
			// 
			this.colFunctionMethod.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colFunctionMethod.DisplayIndex = 4;
			this.colFunctionMethod.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colFunctionMethod.Text = "% in\nMethod";
			this.colFunctionMethod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colFunctionMethod.ToolTip = "% in Method Tool Tip";
			this.colFunctionMethod.Width = 70;
			// 
			// colFunctionChildren
			// 
			this.colFunctionChildren.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colFunctionChildren.DisplayIndex = 5;
			this.colFunctionChildren.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colFunctionChildren.Text = "% in\nChildren";
			this.colFunctionChildren.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colFunctionChildren.ToolTip = "% in Children Tool Tip";
			this.colFunctionChildren.Width = 70;
			// 
			// colFunctionSuspended
			// 
			this.colFunctionSuspended.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colFunctionSuspended.DisplayIndex = 6;
			this.colFunctionSuspended.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colFunctionSuspended.Text = "% Suspended";
			this.colFunctionSuspended.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colFunctionSuspended.ToolTip = "% Suspended Tool Tip";
			this.colFunctionSuspended.Width = 70;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 253);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(677, 3);
			this.splitter2.TabIndex = 23;
			this.splitter2.TabStop = false;
			// 
			// _tcCalls
			// 
			this._tcCalls.Controls.Add(this._tpCallees);
			this._tcCalls.Controls.Add(this._tcCallers);
			this._tcCalls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._tcCalls.Location = new System.Drawing.Point(0, 256);
			this._tcCalls.Name = "_tcCalls";
			this._tcCalls.SelectedIndex = 0;
			this._tcCalls.Size = new System.Drawing.Size(677, 176);
			this._tcCalls.TabIndex = 22;
			// 
			// _tpCallees
			// 
			this._tpCallees.Controls.Add(this._lvCalleesInfo);
			this._tpCallees.Location = new System.Drawing.Point(4, 22);
			this._tpCallees.Name = "_tpCallees";
			this._tpCallees.Size = new System.Drawing.Size(669, 150);
			this._tpCallees.TabIndex = 0;
			this._tpCallees.Text = "Callees";
			// 
			// _lvCalleesInfo
			// 
			this._lvCalleesInfo.AllowColumnReorder = true;
			this._lvCalleesInfo.CaptureFocusClick = false;
			this._lvCalleesInfo.Columns.AddRange(new DotNetLib.Windows.Forms.ToggleColumnHeader[] {
																																  this.colCalleeID,
																																  this.colCalleeSignature,
																																  this.colCalleeCalls,
																																  this.colCalleeTotal,
																																  this.colCalleeParent});
			this._lvCalleesInfo.ColumnSortColor = System.Drawing.Color.WhiteSmoke;
			this._lvCalleesInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this._lvCalleesInfo.HeaderHeight = 33;
			this._lvCalleesInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
			this._lvCalleesInfo.HideSelection = false;
			this._lvCalleesInfo.Location = new System.Drawing.Point(0, 0);
			this._lvCalleesInfo.Name = "_lvCalleesInfo";
			this._lvCalleesInfo.Size = new System.Drawing.Size(669, 150);
			this._lvCalleesInfo.TabIndex = 17;
			this._lvCalleesInfo.DoubleClick += new System.EventHandler(this._lvChildInfo_DoubleClick);
			// 
			// colCalleeID
			// 
			this.colCalleeID.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Integer;
			this.colCalleeID.Text = "ID";
			this.colCalleeID.ToolTip = "ID Tool Tip";
			this.colCalleeID.Width = 100;
			// 
			// colCalleeSignature
			// 
			this.colCalleeSignature.DisplayIndex = 1;
			this.colCalleeSignature.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.String;
			this.colCalleeSignature.Text = "Signature";
			this.colCalleeSignature.ToolTip = "Signature Tool Tip";
			this.colCalleeSignature.Width = 400;
			// 
			// colCalleeCalls
			// 
			this.colCalleeCalls.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colCalleeCalls.DisplayIndex = 2;
			this.colCalleeCalls.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colCalleeCalls.Text = "# of Calls";
			this.colCalleeCalls.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCalleeCalls.ToolTip = "# of Calls Tool Tip";
			this.colCalleeCalls.Width = 70;
			// 
			// colCalleeTotal
			// 
			this.colCalleeTotal.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colCalleeTotal.DisplayIndex = 3;
			this.colCalleeTotal.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colCalleeTotal.Text = "% of Total";
			this.colCalleeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCalleeTotal.ToolTip = "% of Total Tool Tip";
			this.colCalleeTotal.Width = 70;
			// 
			// colCalleeParent
			// 
			this.colCalleeParent.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colCalleeParent.DisplayIndex = 4;
			this.colCalleeParent.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colCalleeParent.Text = "% of Parent";
			this.colCalleeParent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCalleeParent.ToolTip = "% of Parent Tool Tip";
			this.colCalleeParent.Width = 70;
			// 
			// _tcCallers
			// 
			this._tcCallers.Controls.Add(this._lvCallersInfo);
			this._tcCallers.Location = new System.Drawing.Point(4, 22);
			this._tcCallers.Name = "_tcCallers";
			this._tcCallers.Size = new System.Drawing.Size(669, 150);
			this._tcCallers.TabIndex = 1;
			this._tcCallers.Text = "Callers";
			// 
			// _lvCallersInfo
			// 
			this._lvCallersInfo.AllowColumnReorder = true;
			this._lvCallersInfo.CaptureFocusClick = false;
			this._lvCallersInfo.Columns.AddRange(new DotNetLib.Windows.Forms.ToggleColumnHeader[] {
																																  this.colCallerID,
																																  this.colCallerSignature,
																																  this.colCallerCalls,
																																  this.colCallerTotal,
																																  this.colCallerParent});
			this._lvCallersInfo.ColumnSortColor = System.Drawing.Color.WhiteSmoke;
			this._lvCallersInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this._lvCallersInfo.HeaderHeight = 33;
			this._lvCallersInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
			this._lvCallersInfo.HideSelection = false;
			this._lvCallersInfo.Location = new System.Drawing.Point(0, 0);
			this._lvCallersInfo.Name = "_lvCallersInfo";
			this._lvCallersInfo.Size = new System.Drawing.Size(669, 150);
			this._lvCallersInfo.TabIndex = 18;
			this._lvCallersInfo.DoubleClick += new System.EventHandler(this._lvChildInfo_DoubleClick);
			// 
			// colCallerID
			// 
			this.colCallerID.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Integer;
			this.colCallerID.Text = "ID";
			this.colCallerID.ToolTip = "ID Tool Tip";
			this.colCallerID.Width = 100;
			// 
			// colCallerSignature
			// 
			this.colCallerSignature.DisplayIndex = 1;
			this.colCallerSignature.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.String;
			this.colCallerSignature.Text = "Signature";
			this.colCallerSignature.ToolTip = "Signature Tool Tip";
			this.colCallerSignature.Width = 400;
			// 
			// colCallerCalls
			// 
			this.colCallerCalls.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colCallerCalls.DisplayIndex = 2;
			this.colCallerCalls.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colCallerCalls.Text = "# of Calls";
			this.colCallerCalls.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCallerCalls.ToolTip = "# of Calls Tool Tip";
			this.colCallerCalls.Width = 70;
			// 
			// colCallerTotal
			// 
			this.colCallerTotal.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colCallerTotal.DisplayIndex = 3;
			this.colCallerTotal.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colCallerTotal.Text = "% of Total";
			this.colCallerTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCallerTotal.ToolTip = "% of Total Tool Tip";
			this.colCallerTotal.Width = 70;
			// 
			// colCallerParent
			// 
			this.colCallerParent.DefaultSortOrder = System.Windows.Forms.SortOrder.Descending;
			this.colCallerParent.DisplayIndex = 4;
			this.colCallerParent.SortingMethod = DotNetLib.Windows.Forms.SortingMethod.Float;
			this.colCallerParent.Text = "% of Parent";
			this.colCallerParent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCallerParent.ToolTip = "% of Parent Tool Tip";
			this.colCallerParent.Width = 70;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(192, 24);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 432);
			this.splitter1.TabIndex = 9;
			this.splitter1.TabStop = false;
			// 
			// _tvNamespaceInfo
			// 
			this._tvNamespaceInfo.CheckBoxes = true;
			this._tvNamespaceInfo.Dock = System.Windows.Forms.DockStyle.Left;
			this._tvNamespaceInfo.FullRowSelect = true;
			this._tvNamespaceInfo.ImageIndex = -1;
			this._tvNamespaceInfo.Location = new System.Drawing.Point(0, 24);
			this._tvNamespaceInfo.Name = "_tvNamespaceInfo";
			this._tvNamespaceInfo.SelectedImageIndex = -1;
			this._tvNamespaceInfo.Size = new System.Drawing.Size(192, 432);
			this._tvNamespaceInfo.TabIndex = 8;
			this._tvNamespaceInfo.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this._tvNamespaceInfo_AfterCheck);
			this._tvNamespaceInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._tvNamespaceInfo_AfterSelect);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this._lblFilterSignatures);
			this.panel2.Controls.Add(this._txtFilterBar);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this._cbCurrentThread);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(872, 24);
			this.panel2.TabIndex = 12;
			// 
			// _lblFilterSignatures
			// 
			this._lblFilterSignatures.AutoSize = true;
			this._lblFilterSignatures.Location = new System.Drawing.Point(200, 4);
			this._lblFilterSignatures.Name = "_lblFilterSignatures";
			this._lblFilterSignatures.Size = new System.Drawing.Size(88, 16);
			this._lblFilterSignatures.TabIndex = 14;
			this._lblFilterSignatures.Text = "Filter signatures:";
			// 
			// _txtFilterBar
			// 
			this._txtFilterBar.Location = new System.Drawing.Point(288, 0);
			this._txtFilterBar.Multiline = true;
			this._txtFilterBar.Name = "_txtFilterBar";
			this._txtFilterBar.Size = new System.Drawing.Size(360, 20);
			this._txtFilterBar.TabIndex = 13;
			this._txtFilterBar.Text = "";
			this._txtFilterBar.TextChanged += new System.EventHandler(this._txtFilterBar_TextChanged);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 12;
			this.label1.Text = "Thread:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _cbCurrentThread
			// 
			this._cbCurrentThread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbCurrentThread.Location = new System.Drawing.Point(48, 0);
			this._cbCurrentThread.Name = "_cbCurrentThread";
			this._cbCurrentThread.Size = new System.Drawing.Size(136, 21);
			this._cbCurrentThread.TabIndex = 11;
			this._cbCurrentThread.SelectedIndexChanged += new System.EventHandler(this._cbCurrentThread_SelectedIndexChanged);
			// 
			// _tmrFilterThrottle
			// 
			this._tmrFilterThrottle.Interval = 300;
			this._tmrFilterThrottle.Tick += new System.EventHandler(this._tmrFilterThrottle_Tick);
			// 
			// ProfilerControl
			// 
			this.Controls.Add(this.panel1);
			this.Name = "ProfilerControl";
			this.Size = new System.Drawing.Size(872, 456);
			this.Load += new System.EventHandler(this.ProfilerControl_Load);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this._tcCalls.ResumeLayout(false);
			this._tpCallees.ResumeLayout(false);
			this._tcCallers.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public Run ProfilerRun
		{
			set 
			{ 
				_tic = value.ThreadInfoCollection; 
				RefreshData(); 
			}
		}

		private void RefreshData()
		{
			this.BeginInvoke( new EventHandler( UpdateOnUIThread ), new object[] { null, null } );
		}

		private void UpdateOnUIThread( object oSender, EventArgs ea )
		{
			_cbCurrentThread.Items.Clear();

			_cbCurrentThread.Sorted = false;
			SortedList sl = new SortedList();

			// Sort the thread info
			foreach ( ThreadInfo ti in _tic )
				sl.Add( ti.ID, ti );

			// Then add them to the combobox
			foreach ( DictionaryEntry de in sl )
				_cbCurrentThread.Items.Add( de.Value );

			if ( _cbCurrentThread.Items.Count > 0 )
			{
				_cbCurrentThread.SelectedIndex = 0;
				_cbCurrentThread.Enabled = true;
			}
			else
				_cbCurrentThread.Enabled = false;
		}

		private void _cbCurrentThread_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateTree();
			UpdateFilters();
		}

		private void UpdateTree()
		{
			_tvNamespaceInfo.Nodes.Clear();

			_bUpdating = true;
			_htCheckedItems = new Hashtable();

			try
			{
				_tvNamespaceInfo.BeginUpdate();

				TreeNode tnRoot = _tvNamespaceInfo.Nodes.Add( "All Namespaces" );
				tnRoot.Tag = "";
				tnRoot.Checked = true;

				ThreadInfo tiCurrentThread = _tic[ ( ( ThreadInfo )_cbCurrentThread.SelectedItem ).ID ];
				foreach ( FunctionInfo fi in tiCurrentThread.FunctionInfoCollection )
				{
					TreeNodeCollection tnc = tnRoot.Nodes;
					ArrayList alNamespace = new ArrayList();
					foreach ( string strNamespacePiece in fi.Signature.Namespace )
					{
						alNamespace.Add( strNamespacePiece );
						TreeNode tnFound = null;

						foreach ( TreeNode tn in tnc )
						{
							if ( tn.Text == strNamespacePiece )
							{
								tnFound = tn;
								break;
							}
						}

						if ( tnFound == null )
						{
							tnFound = tnc.Add( strNamespacePiece );
							tnFound.Tag = String.Join( ".", ( string[] )alNamespace.ToArray( typeof( string ) ) );
							tnFound.Checked = true;
						}

						tnc = tnFound.Nodes;
					}
				}

				tnRoot.Expand();
			}
			finally
			{
				_bUpdating = false;
				_tvNamespaceInfo.EndUpdate();
			}
		}

		private void UpdateFilters()
		{
			_lvFunctionInfo.Items.Clear();
			_lvCalleesInfo.Items.Clear();
			_lvCallersInfo.Items.Clear();

			Hashtable htNamespaces = new Hashtable();
			foreach ( TreeNode tn in _htCheckedItems.Keys )
				htNamespaces[ tn.Tag ] = null;

			try
			{
				_lvFunctionInfo.BeginUpdate();

				ThreadInfo tiCurrentThread = _tic[ ( ( ThreadInfo )_cbCurrentThread.SelectedItem ).ID ];
				foreach ( FunctionInfo fi in tiCurrentThread.FunctionInfoCollection )
				{
					if ( !htNamespaces.Contains( fi.Signature.NamespaceString ) )
						continue;

					ContainerListViewItem lvi = _lvFunctionInfo.Items.Add( fi.ID.ToString() );
					lvi.SubItems[ 1 ].Text = fi.Signature.Signature;
					lvi.SubItems[ 2 ].Text = fi.Calls.ToString();
					lvi.SubItems[ 3 ].Text = fi.PercentOfTotalTimeInMethodAndChildren.ToString( "0.00;-0.00;0" );
					lvi.SubItems[ 4 ].Text = fi.PercentOfTotalTimeInMethod.ToString( "0.00;-0.00;0" );
					lvi.SubItems[ 5 ].Text = fi.PercentOfTotalTimeInChildren.ToString( "0.00;-0.00;0" );
					lvi.SubItems[ 6 ].Text = fi.PercentOfTotalTimeSuspended.ToString( "0.00;-0.00;0" );
					lvi.Tag = fi;
				}
			}
			finally
			{
				_lvFunctionInfo.Sort();
				_lvFunctionInfo.EndUpdate();
			}
		}


		private void _tvNamespaceInfo_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			UpdateFilters();
		}

		private void _tvNamespaceInfo_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			bool bInCheck = _bInCheck;

			if ( !bInCheck )
				_bInCheck = true;

			try
			{
				if ( e.Node.Checked )
					_htCheckedItems[ e.Node ] = null;
				else
					_htCheckedItems.Remove( e.Node );
				if ( _bUpdating )
					return;

				foreach ( TreeNode tnChild in e.Node.Nodes )
					tnChild.Checked = e.Node.Checked;

				if ( bInCheck )
					return;
			}
			finally
			{
				if ( !bInCheck )
					_bInCheck = false;
			}

			UpdateFilters();
		}

		private void _lvFunctionInfo_SelectedItemsChanged(object sender, System.EventArgs e)
		{
			_lvCalleesInfo.Items.Clear();
			_lvCallersInfo.Items.Clear();

			if ( _lvFunctionInfo.SelectedItems.Count == 0 )
				return;

			// somebody clicked! empty the forward stack and push this click on the "Back" stack.
			if ( !_bNavigating )
			{
				_navStackForward.Clear();
				if ( _navCurrent != null )
					_navStackBack.Push( _navCurrent );

				ArrayList lst = new ArrayList();

				for( int idx = 0; idx < _lvFunctionInfo.SelectedItems.Count; ++idx )
					if( _lvFunctionInfo.SelectedItems[idx].Tag != null )
						lst.Add( ( _lvFunctionInfo.SelectedItems[idx].Tag as FunctionInfo ).ID );

				_navCurrent = ( int[] )lst.ToArray( typeof( int ) );
			}

			UpdateCalleeList();
			UpdateCallerList();
		}

		private void UpdateCallerList()
		{
			_lvCallersInfo.BeginUpdate();

			bool multipleSelected = (_lvFunctionInfo.SelectedItems.Count > 1);
			_lvCallersInfo.ShowPlusMinus = multipleSelected;
			_lvCallersInfo.ShowRootTreeLines = multipleSelected;
			_lvCallersInfo.ShowTreeLines = multipleSelected;

			ThreadInfo tiCurrentThread = _tic[ ( ( ThreadInfo )_cbCurrentThread.SelectedItem ).ID ];
			foreach ( ContainerListViewItem item in _lvFunctionInfo.SelectedItems )
			{
				FunctionInfo mfi = ( FunctionInfo )item.Tag;

				foreach ( FunctionInfo fi in tiCurrentThread.FunctionInfoCollection )
					foreach ( CalleeFunctionInfo cfi in fi.CalleeInfo )
						if ( cfi.ID == mfi.ID )
						{
							ContainerListViewItem parentItem = null;

							foreach ( ContainerListViewItem pitem in _lvCallersInfo.Items )
								if ( ( pitem.Tag as CalleeFunctionInfo ).ID == fi.ID )
									parentItem = pitem;

							if ( parentItem == null ) // don't have it
							{
								parentItem = _lvCallersInfo.Items.Add( fi.ID.ToString() );
								parentItem.SubItems[ 1 ].Text = fi.Signature.Signature;
								parentItem.SubItems[ 2 ].Text = cfi.Calls.ToString();
								parentItem.SubItems[ 3 ].Text = cfi.PercentOfTotalTimeInMethod.ToString( "0.00;-0.00;0" );
								parentItem.SubItems[ 4 ].Text = cfi.PercentOfParentTimeInMethod.ToString( "0.00;-0.00;0" );
								parentItem.Tag = cfi;
							}
							else // do, update totals
							{
								parentItem.SubItems[ 2 ].Text = (int.Parse( parentItem.SubItems[ 2 ].Text ) + cfi.Calls ).ToString();
								parentItem.SubItems[ 3 ].Text = "-";
								parentItem.SubItems[ 4 ].Text = "-";
							}

							// either way, add a child pointing back to the parent
							ContainerListViewItem lvi = parentItem.Items.Add( cfi.ID.ToString() );
							lvi.SubItems[ 1 ].Text = cfi.Signature;
							lvi.SubItems[ 2 ].Text = cfi.Calls.ToString();
							lvi.SubItems[ 3 ].Text = cfi.PercentOfTotalTimeInMethod.ToString( "0.00;-0.00;0" );
							lvi.SubItems[ 4 ].Text = cfi.PercentOfParentTimeInMethod.ToString( "0.00;-0.00;0" );
							lvi.Tag = cfi;
						}
			}

			_lvCallersInfo.Sort();
			_lvCallersInfo.EndUpdate();
		}

		private void UpdateCalleeList()
		{
			_lvCalleesInfo.BeginUpdate();

			bool multipleSelected = (_lvFunctionInfo.SelectedItems.Count > 1);
			_lvCalleesInfo.ShowPlusMinus = multipleSelected;
			_lvCalleesInfo.ShowRootTreeLines = multipleSelected;
			_lvCalleesInfo.ShowTreeLines = multipleSelected;

			ContainerListViewItem lviSuspend = null;

			foreach ( ContainerListViewItem item in _lvFunctionInfo.SelectedItems )
			{
				FunctionInfo fi = ( FunctionInfo )item.Tag;

				foreach ( CalleeFunctionInfo cfi in fi.CalleeInfo )
				{
					ContainerListViewItem parentItem = null;

					foreach ( ContainerListViewItem pitem in _lvCalleesInfo.Items )
						if( pitem.Tag != null)
							if ( ( pitem.Tag as CalleeFunctionInfo ).ID == cfi.ID )
								parentItem = pitem;


					if ( parentItem == null ) // don't have it
					{
						parentItem = _lvCalleesInfo.Items.Add( cfi.ID.ToString() );
						parentItem.SubItems[ 1 ].Text = cfi.Signature;
						parentItem.SubItems[ 2 ].Text = cfi.Calls.ToString();
						parentItem.SubItems[ 3 ].Text = cfi.PercentOfTotalTimeInMethod.ToString( "0.00;-0.00;0" );
						parentItem.SubItems[ 4 ].Text = cfi.PercentOfParentTimeInMethod.ToString( "0.00;-0.00;0" );
						parentItem.Tag = cfi;
					}
					else // do, update totals
					{
						parentItem.SubItems[ 2 ].Text = (int.Parse( parentItem.SubItems[ 2 ].Text ) + cfi.Calls ).ToString();
						parentItem.SubItems[ 3 ].Text = "-";
						parentItem.SubItems[ 4 ].Text = "-";
					}

					// either way, add a child pointing back to the parent
					ContainerListViewItem lvi = parentItem.Items.Add( fi.ID.ToString() );
					lvi.SubItems[ 1 ].Text = fi.Signature.Signature;
					lvi.SubItems[ 2 ].Text = cfi.Calls.ToString();
					lvi.SubItems[ 3 ].Text = cfi.PercentOfTotalTimeInMethod.ToString( "0.00;-0.00;0" );
					lvi.SubItems[ 4 ].Text = cfi.PercentOfParentTimeInMethod.ToString( "0.00;-0.00;0" );
					lvi.Tag = cfi;
				}

				if ( fi.TotalSuspendedTicks > 0 )
				{
					if ( lviSuspend == null) // don't have it
					{
						lviSuspend = _lvCalleesInfo.Items.Add( "(suspend)" );
						lviSuspend.SubItems[ 1 ].Text = "(thread suspended)";
						lviSuspend.SubItems[ 2 ].Text = "-";
						lviSuspend.SubItems[ 3 ].Text = fi.PercentOfTotalTimeSuspended.ToString( "0.00;-0.00;0" );
						lviSuspend.SubItems[ 4 ].Text = fi.PercentOfMethodTimeSuspended.ToString( "0.00;-0.00;0" );
					}
					else // do, update totals
					{
						lviSuspend.SubItems[ 3 ].Text = "-";
						lviSuspend.SubItems[ 4 ].Text = "-";
					}

					// either way, add a child pointing back to the parent
					ContainerListViewItem lvi = lviSuspend.Items.Add( fi.ID.ToString() );
					lvi.SubItems[ 1 ].Text = fi.Signature.Signature;
					lvi.SubItems[ 2 ].Text = "-";
					lvi.SubItems[ 3 ].Text = fi.PercentOfTotalTimeSuspended.ToString( "0.00;-0.00;0" );
					lvi.SubItems[ 4 ].Text = fi.PercentOfMethodTimeSuspended.ToString( "0.00;-0.00;0" );
				}
			}

			_lvCalleesInfo.Sort();
			_lvCalleesInfo.EndUpdate();
		}

		private void ProfilerControl_Load(object sender, System.EventArgs e)
		{
			_lvFunctionInfo.Sort(1, true, true);
			_lvCalleesInfo.Sort(1, true, true);
		}

		private void _lvChildInfo_DoubleClick(object sender, System.EventArgs e)
		{
			ContainerListView ctl = sender as ContainerListView;

			if ( ctl.SelectedItems.Count == 0 )
				return;

			CalleeFunctionInfo cfi = ( CalleeFunctionInfo )ctl.SelectedItems[ 0 ].Tag;
			if ( cfi == null )
				MessageBox.Show( "No information available for this item" );
			else
				JumpToID( cfi.ID );
		}

		private void _txtFilterBar_TextChanged(object sender, System.EventArgs e)
		{
			_tmrFilterThrottle.Enabled = false;
			_tmrFilterThrottle.Enabled = true;
		}

		private void _tmrFilterThrottle_Tick(object sender, System.EventArgs e)
		{
			_tmrFilterThrottle.Enabled = false;
			_lvFunctionInfo.Filter( 1, _txtFilterBar.Text );
		}

		public void  NavigateBackward()
		{
			if (_navStackBack.Count == 0)
				return;

			_navStackForward.Push(_navCurrent);
			_navCurrent = ( int[] ) _navStackBack.Pop();

			_bNavigating = true;
			JumpToID( _navCurrent );
			_bNavigating = false;
		}

		public void NavigateForward()
		{
			if ( _navStackForward.Count == 0 )
				return;

			_navStackBack.Push( _navCurrent );
			_navCurrent = ( int[] ) _navStackForward.Pop();

			_bNavigating = true;
			JumpToID( _navCurrent );
			_bNavigating = false;
		}

		private void JumpToID( int nID )
		{
			JumpToID( new int[] { nID } );
		}

		private void JumpToID( int[] ids )
		{
			_lvFunctionInfo.SelectedItems.Clear();

			foreach( int id in ids )
				foreach( ContainerListViewItem lvi in _lvFunctionInfo.Items )
				{
					FunctionInfo fi = ( FunctionInfo )lvi.Tag;

					if ( fi.ID == id )
					{
						if(!lvi.Selected)
						{
							if(lvi.IsFiltered)
							{
								if(MessageBox.Show("Cannot navigate to that function because it is being filtered.  Would you like to remove the filter and continue to that function?", "Filtered Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
									return;
								else
									_txtFilterBar.Text = string.Empty;
							}

							lvi.Selected = true;
							lvi.Focused = true;
						}
						break;
					}
				}

			_lvFunctionInfo.EnsureVisible();
			_lvFunctionInfo.Focus();
		}

		private int GetSelectedID()
		{
			if ( _lvCalleesInfo.SelectedItems.Count == 0 )
				return -1;

			return ( ( FunctionInfo )_lvFunctionInfo.SelectedItems[ 0 ].Tag ).ID;
		}

		private ThreadInfoCollection _tic;
		private bool _bUpdating, _bInCheck;
		private Hashtable _htCheckedItems;
		private Stack _navStackBack = new Stack();
		private Stack _navStackForward = new Stack();
		private int[] _navCurrent = null;
		private bool _bNavigating = false;

		#region Context Menus

		#region Headers

		private void _lv_HeaderMenuEvent(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ContainerListView listView = sender as ContainerListView;

			PopupMenu pop = new PopupMenu();
			pop.Selected += new CommandHandler(pop_Selected);
			pop.Deselected += new CommandHandler(pop_Deselected);

			MenuCommand sortBy = pop.MenuCommands.Add(new MenuCommand("Sort &By"));
			pop.MenuCommands.Add(new MenuCommand("-"));
			bool isAscending = true;
			for(int idx = 0; idx < listView.Columns.Count; ++idx)
			{
				ToggleColumnHeader hdr = listView.Columns[idx];
				MenuCommand sortByItem = new MenuCommand(hdr.Text);

				sortByItem.Description = string.Format("Sort By the '{1}' column from this grid", (hdr.Visible ? "Shows" : "Hides"), hdr.Text);
				sortByItem.RadioCheck = true;
				sortByItem.Checked = hdr.SortingOrder != SortOrder.None;
				sortByItem.Tag = new object[] { listView, idx };
				//				sortByItem.Click += new EventHandler(sortByItem_Click);

				if(sortByItem.Checked)
					isAscending = hdr.SortingOrder == SortOrder.Ascending;

				sortBy.MenuCommands.Add(sortByItem);
			}

			sortBy.MenuCommands.Add(new MenuCommand("-"));
			MenuCommand ascending = sortBy.MenuCommands.Add(new MenuCommand("&Ascending"));
			ascending.RadioCheck = true;
			ascending.Checked = isAscending;
			ascending.Tag = new object[] { listView, SortOrder.Ascending };
			//			ascending.Click += new EventHandler(sortOrder_Click);

			MenuCommand descending = sortBy.MenuCommands.Add(new MenuCommand("&Descending"));
			descending.RadioCheck = true;
			descending.Checked = !isAscending;
			descending.Tag = new object[] { listView, SortOrder.Descending };
			//			descending.Click += new EventHandler(sortOrder_Click);

			bool allShown = true;
			for(int idx = 0; idx < listView.Columns.Count; ++idx)
			{
				ToggleColumnHeader hdr = listView.Columns[idx];
				MenuCommand checkable = new MenuCommand(hdr.Text);

				checkable.Description = string.Format("{0} the '{1}' column from this grid", (hdr.Visible ? "Shows" : "Hides"), hdr.Text);
				checkable.Checked = hdr.Visible;
				checkable.Tag = hdr;

				pop.MenuCommands.Add(checkable);
				allShown &= hdr.Visible;
			}

			pop.MenuCommands.Add(new MenuCommand("-"));
			pop.MenuCommands.Add(new MenuCommand("Show &All")).Enabled = !allShown;

			MenuCommand result = pop.TrackPopup(listView.PointToScreen(new Point(e.X, e.Y)));
			if(result != null && result.Tag is ToggleColumnHeader)
				(result.Tag as ToggleColumnHeader).Visible = !result.Checked;
		}

		//		private void sortOrder_Click(object sender, EventArgs e)
		//		{
		//			MenuCommand cmd = sender as MenuCommand;
		//
		//			object[] objs = (cmd.Tag as object[]);
		//			ContainerListView listView = objs[0] as ContainerListView;
		//
		//			listView.Sort((SortOrder)objs[1]);
		//		}

		//		private void sortByItem_Click(object sender, EventArgs e)
		//		{
		//			MenuCommand cmd = sender as MenuCommand;
		//
		//			object[] objs = (cmd.Tag as object[]);
		//			ContainerListView listView = objs[0] as ContainerListView;
		//
		//			listView.Sort((int)objs[1], false);
		//		}

		#endregion

		private void pop_Selected(MenuCommand item)
		{
			// TODO: Update status bar with item.Description
		}

		private void pop_Deselected(MenuCommand item)
		{
			// TODO: Update status bar with string.Empty
		}

		#endregion
	}
}
