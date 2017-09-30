using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Ekstrand.Windows.Forms
{
    /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileDirSelector/*" />
    [
        ToolboxBitmap(typeof(FileDirSelector), "Resources.FileDirSelector"),
        ToolboxItemFilter("Ekstrand.Windows.Forms"),
        ToolboxItem(true),        
        Designer(typeof(FileDirSelectorlDesigner)),
        Description("File & Dir Selector")
    ]
    public sealed partial class FileDirSelector : UserControl
    {
        #region Fields

        private static readonly object EventHelpRequest = new object();

        private BorderStyle m_BoarderStyle = BorderStyle.None;
        private ButtonSide m_ButtonSide = ButtonSide.Right;
        private FileDirDialogType m_FileDirDialog;
        private FolderBrowserDialog m_FolderBrowserDialog;
        private bool m_IsDialogOpen = false;
        private OpenFileDialog m_OpenFileDialog;

        #endregion Fields

        #region Constructors

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileDirSelectorConstructor/*" />
        public FileDirSelector()
        {
            InitializeComponent();

            this.ButtonSideChanged += FileDirSelector_ButtonSideChanged;
            DialogType = FileDirDialogType.OpenFileDialog;
            DisplayShortenPath = true;

            this.textBox1.AutoSize = false;
            this.textBox1.HideSelection = false;

            Undo.Click += Undo_Click;
            Copy.Click += Copy_Click;
            Delete.Click += Delete_Click;
            SelectAll.Click += SelectAll_Click;
            Paste.Click += Paste_Click;
            Cut.Click += Cut_Click;
        }

        #endregion Constructors

        #region Properties

        /// <include file="..\ClassDocs\CodeDocElements.xml" path='Comments/AddExtension/*' />
        [
            Category("Behavior"),
            DefaultValue(true),
            Description("Controls whether extensions are automatically add to file names.")
        ]
        public bool AddExtension
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.AddExtension;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.AddExtension = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/AutoUpgradeEnabled/*" />
        [
            DefaultValue(true)
        ]
        public bool AutoUpgradeEnabled
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.AutoUpgradeEnabled;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.AutoUpgradeEnabled = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/BoarderStyle/*" />
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public new BorderStyle BorderStyle
        {
            get
            {
                return m_BoarderStyle;
            }
            set
            {
                m_BoarderStyle = value;
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ButtonSide/*" />
        [
            Category("Appearance"),
            Description("Defines the position of the selector button on the control."),
            Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
        ]
        public ButtonSide ButtonSide
        {
            get
            {
                return m_ButtonSide;
            }
            set
            {
                if (value != m_ButtonSide)
                {
                    m_ButtonSide = value;
                    OnButtonSideChanged(new EventArgs());
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/CheckFileExists/*" />
        [
            Category("Behavior"),
            DefaultValue(false),
            Description("Indicates whether a warner appears with in the user specifies a file that does not exist.")
        ]
        public bool CheckFileExists
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.CheckFileExists;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.CheckFileExists = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/CheckPathExists/*" />
        [
            Category("Behavior"),
            DefaultValue(true),
            Description("Indicates whether the dialog box displays a warning if the user specifies a path that does not exist.")
        ]
        public bool CheckPathExists
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.CheckPathExists;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.CheckPathExists = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/CustomPlaces/*" />
        [Browsable(false)]
        public FileDialogCustomPlacesCollection CustomPlaces
        {
            get
            {
                return m_OpenFileDialog.CustomPlaces;
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/DefaultExt/*" />
        [
            Category("Behavior"),
            DefaultValue(""),
            Description("The default file extension.")
        ]
        public string DefaultExt
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.DefaultExt;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.DefaultExt = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/DereferenceLinks/*" />
        [
           Category("Behavior"),
           DefaultValue(true),
           Description("Indicates whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).")
       ]
        public bool DereferenceLinks
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.DereferenceLinks;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.DereferenceLinks = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileDirDialog/*" />
        [
            Category("Behavior"),
            Description("Defines which dialog to be shown to the user"),
            Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
        ]
        public FileDirDialogType DialogType
        {
            get
            {
                return m_FileDirDialog;
            }

            set
            {
                m_FileDirDialog = value;
                if (m_FileDirDialog == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog = new OpenFileDialog();
                    m_OpenFileDialog.HelpRequest += M_OpenFileDialog_HelpRequest;
                    m_FolderBrowserDialog = null;
                }
                else
                {
                    m_OpenFileDialog = null;
                    m_FolderBrowserDialog = new FolderBrowserDialog();
                }
            }
        }

        /// <include file="..\ClassDocs\CodeDocElements.xml" path='Comments/DisplayShortenPath/*' />
        [
            Category("Behavior"),
            DefaultValue(true),
            Description("Display shorten Path in text box.")
        ]
        public bool DisplayShortenPath
        {
            get;set;
        }
        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileName/*" />
        [
            Category("Data"),
            DefaultValue(""),
            Description(" The file name selected in the file dialog box.")
        ]
        public string FileName
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.FileName = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileNames/*" />
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Description("Names of all selected files in the dialog box.")
        ]
        public string[] FileNames
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.FileNames;
                }
                else
                {
                    return new string[0];
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Filter/*" />
        [
            Category("Behavior"),
            DefaultValue(""),
            Localizable(true),
            Description("File name filter string, which determines the choices that appear in the \"Save as file type\" or \"Files of type\" box in the dialog box.")
        ]
        public string Filter
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.Filter;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.Filter = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FilterIndex/*" />
        [
            Category("Behavior"),
            DefaultValue(1),
            Description("The index of the filter currently selected in the file dialog box.")
        ]
        public int FilterIndex
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.FilterIndex;
                }
                else
                {
                    return int.MinValue;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.FilterIndex = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/InitialDirectory/*" />
        [
            Category("Data"),
            DefaultValue(""),
            Description("The initial directory displayed by the file dialog box.")
        ]
        public string InitialDirectory
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.InitialDirectory;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.InitialDirectory = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Multiselect/*" />
        [
            Category("Behavior"),
            DefaultValue(false),
            Description("Value indicating whether the dialog box allows multiple files to be selected.")
        ]
        public bool Multiselect
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.Multiselect;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.Multiselect = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ReadOnlyChecked/*" />
        [
           Category("Behavior"),
           DefaultValue(false),
           Description("Value indicating whether the read-only check box is selected.")
        ]
        public bool ReadOnlyChecked
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.ReadOnlyChecked;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.ReadOnlyChecked = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/RestoreDirectory/*" />
        [
            Category("Behavior"),
            DefaultValue(false),
            Description("A value indicating whether the dialog box restores the current directory before closing.")
        ]
        public bool RestoreDirectory
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.RestoreDirectory;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.RestoreDirectory = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/RootFolder/*" />
        [BrowsableAttribute(true)]
        public Environment.SpecialFolder RootFolder
        {
            get
            {
                if (m_FolderBrowserDialog != null)
                {
                    return m_FolderBrowserDialog.RootFolder;
                }
                else
                {
                    return Environment.SpecialFolder.MyComputer;
                }
            }

            set
            {
                if (m_FolderBrowserDialog != null)
                {
                    m_FolderBrowserDialog.RootFolder = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/SafeFileName/*" />
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")
        ]
        public string SafeFileName
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.SafeFileName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/SafeFileNames/*" />
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays"),
            SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")
        ]
        public string[] SafeFileNames
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.SafeFileNames;
                }
                else
                {
                    return new string[0];
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/SelectedPath/*" />
        public string SelectedPath
        {
            get
            {
                if (m_FolderBrowserDialog != null)
                {
                    return m_FolderBrowserDialog.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                if (m_FolderBrowserDialog != null)
                {
                    m_FolderBrowserDialog.SelectedPath = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ShowHelp/*" />
        [
            Category("Behavior"),
            DefaultValue(false),
            Description("Controls whether the Help button is displayed in the file dialog.")
        ]
        public bool ShowHelp
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.ShowHelp;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.ShowHelp = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ShowNewFolderButton/*" />
        [
            Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
        ]
        public bool ShowNewFolderButton
        {
            get
            {
                if (m_FolderBrowserDialog != null)
                {
                    return m_FolderBrowserDialog.ShowNewFolderButton;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                if (m_FolderBrowserDialog != null)
                {
                    m_FolderBrowserDialog.ShowNewFolderButton = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ShowReadOnly/*" />
        [
            Category("Behavior"),
            DefaultValue(false),
            Description("Value indicating whether the dialog contains a read-only check box.")
        ]
        public bool ShowReadOnly
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.ShowReadOnly;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.ShowReadOnly = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/SupportMultiDottedExtensions/*" />
        [
            Category("Behavior"),
            DefaultValue(false),
            Description("Controls whether def or abc.def is the extension of the file filename.abc.def")
        ]
        public bool SupportMultiDottedExtensions
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.SupportMultiDottedExtensions;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.SupportMultiDottedExtensions = value;
                }
            }
        }

        /// <include file="..\ClassDocs\CodeDocElements.xml" path="Comments/Text/*" />
        public override string Text
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.FileName;
                }
                else
                {
                    return m_FolderBrowserDialog.SelectedPath;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    textBox1.Text = value;
                }
                else
                {
                    textBox1.Text = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Title/*" />
        [
            Category("Appearance"),
            DefaultValue(""),
            Localizable(true),
            Description("Display the file dialog box title.")
        ]
        public string Title
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.Title;
                }
                else
                {
                    return m_FolderBrowserDialog.Description;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.Title = value;
                }
                else
                {
                    m_FolderBrowserDialog.Description = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ValidateNames/*" />
        [
            Category("Behavior"),
            DefaultValue(true),
            Description("Value indicating whether the dialog box accepts only valid Win32 file names.")
        ]
        public bool ValidateNames
        {
            get
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    return m_OpenFileDialog.ValidateNames;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (DialogType == FileDirDialogType.OpenFileDialog)
                {
                    m_OpenFileDialog.ValidateNames = value;
                }
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/DefaultMinimumSize/*" />
        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(60, 20);
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/DefaultSize/*" />
        protected override Size DefaultSize
        {
            get
            {
                return new Size(120, 21);
            }
        }

        #endregion Properties

        #region Methods

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Reset/*" />
        public void Reset()
        {
            m_OpenFileDialog.Reset();
            textBox1.Clear();
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ToString/*" />
        public override string ToString()
        {
            if (Text != "")
            {
                return base.ToString();
            }
            return Text;
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/OnFontChanged/*" />
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            int height = this.textBox1.Height;
            this.button1.Height = height + 2;
            this.button1.Location = new Point(this.button1.Location.X, -1);
            this.Height = height;
        }

        /*
         * From Stackoverflow https://stackoverflow.com/questions/1764204/how-to-display-abbreviated-path-names-in-net
         * By Torben Warberg Rohde
         */

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ShortenPath/*" />
        private static string ShortenPath(string path, int maxLength)
        {
            string ellipsisChars = "...";
            char dirSeperatorChar = Path.DirectorySeparatorChar;
            string directorySeperator = dirSeperatorChar.ToString();

            //simple guards
            if (path.Length <= maxLength)
            {
                return path;
            }
            int ellipsisLength = ellipsisChars.Length;
            if (maxLength <= ellipsisLength)
            {
                return ellipsisChars;
            }

            //alternate between taking a section from the start (firstPart) or the path and the end (lastPart)
            bool isFirstPartsTurn = true; //drive letter has first priority, so start with that and see what else there is room for

            //vars for accumulating the first and last parts of the final shortened path
            string firstPart = "";
            string lastPart = "";
            //keeping track of how many first/last parts have already been added to the shortened path
            int firstPartsUsed = 0;
            int lastPartsUsed = 0;

            string[] pathParts = path.Split(dirSeperatorChar);
            for (int i = 0; i < pathParts.Length; i++)
            {
                if (isFirstPartsTurn)
                {
                    string partToAdd = pathParts[firstPartsUsed] + directorySeperator;
                    if ((firstPart.Length + lastPart.Length + partToAdd.Length + ellipsisLength) > maxLength)
                    {
                        break;
                    }
                    firstPart = firstPart + partToAdd;
                    if (partToAdd == directorySeperator)
                    {
                        //this is most likely the first part of and UNC or relative path
                        //do not switch to lastpart, as these are not "true" directory seperators
                        //otherwise "\\myserver\theshare\outproject\www_project\file.txt" becomes "\\...\www_project\file.txt" instead of the intended "\\myserver\...\file.txt")
                    }
                    else
                    {
                        isFirstPartsTurn = false;
                    }
                    firstPartsUsed++;
                }
                else
                {
                    int index = pathParts.Length - lastPartsUsed - 1; //-1 because of length vs. zero-based indexing
                    string partToAdd = directorySeperator + pathParts[index];
                    if ((firstPart.Length + lastPart.Length + partToAdd.Length + ellipsisLength) > maxLength)
                    {
                        break;
                    }
                    lastPart = partToAdd + lastPart;
                    if (partToAdd == directorySeperator)
                    {
                        //this is most likely the last part of a relative path (e.g. "\websites\myproject\www_myproj\App_Data\")
                        //do not proceed to processing firstPart yet
                    }
                    else
                    {
                        isFirstPartsTurn = true;
                    }
                    lastPartsUsed++;
                }
            }

            if (lastPart == "")
            {
                //the filename (and root path) in itself was longer than maxLength, shorten it
                lastPart = pathParts[pathParts.Length - 1];//"pathParts[pathParts.Length -1]" is the equivalent of "Path.GetFileName(pathToShorten)"
                lastPart = lastPart.Substring(lastPart.Length + ellipsisLength + firstPart.Length - maxLength, maxLength - ellipsisLength - firstPart.Length);
            }

            return firstPart + ellipsisChars + lastPart;
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/button1_Click/*" />
        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_IsDialogOpen)
            {
                m_IsDialogOpen = true;
                if (m_FileDirDialog == FileDirDialogType.OpenFileDialog)
                {           
                    if (m_OpenFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        if (m_OpenFileDialog.FileName != "")
                        {
                            Text = DisplayShortenPath == true ? ShortenPath(m_OpenFileDialog.FileName, TextBoxVisiableChars()): m_OpenFileDialog.FileName;
                        }
                        else if (m_OpenFileDialog.FileNames.Length > 0)
                        {
                            Text = DisplayShortenPath == true ? ShortenPath(m_OpenFileDialog.FileNames[0], TextBoxVisiableChars()) : m_OpenFileDialog.FileNames[0];
                        }
                    }
                }
                else
                {
                    if (m_FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        if (m_FolderBrowserDialog.SelectedPath != "")
                        {
                            
                            Text = DisplayShortenPath == true ? ShortenPath(m_FolderBrowserDialog.SelectedPath, TextBoxVisiableChars()): m_FolderBrowserDialog.SelectedPath;
                        }
                    }
                }
                m_IsDialogOpen = false;
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/contextMenuStrip1_Opening/*" />
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // undo is not supported
            contextMenuStrip1.Items["Undo"].Enabled = false;

            // Disable Cut, Copy and Delete if any text is not selected in TextBox
            if (textBox1.SelectedText.Length == 0)
            {
                contextMenuStrip1.Items["Cut"].Enabled = false;
                contextMenuStrip1.Items["Copy"].Enabled = false;
                contextMenuStrip1.Items["Delete"].Enabled = false;
            }
            else
            {
                contextMenuStrip1.Items["Cut"].Enabled = true;
                contextMenuStrip1.Items["Copy"].Enabled = true;
                contextMenuStrip1.Items["Delete"].Enabled = true;
            }

            // Disable Paste if Clipboard does not contains text
            if (Clipboard.ContainsText())
            {
                contextMenuStrip1.Items["Paste"].Enabled = true;
            }
            else
            {
                contextMenuStrip1.Items["Paste"].Enabled = false;
            }

            // Disable Select All if TextBox is blank
            if (textBox1.Text.Length == 0)
            {
                contextMenuStrip1.Items["SelectAll"].Enabled = false;
            }
            else
            {
                contextMenuStrip1.Items["SelectAll"].Enabled = true;
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Copy_Click/*" />
        private void Copy_Click(object sender, EventArgs e)
        {
            if (m_FileDirDialog == FileDirDialogType.OpenFileDialog)
            {
                Clipboard.SetText(m_OpenFileDialog.FileName);
            }
            else
            {
                Clipboard.SetText(string.Copy(m_FolderBrowserDialog.SelectedPath));
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Cut_Click/*" />
        private void Cut_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Delete_Click/*" />
        private void Delete_Click(object sender, EventArgs e)
        {
            int SelectionIndex = textBox1.SelectionStart;
            int SelectionCount = textBox1.SelectionLength;
            textBox1.Text = textBox1.Text.Remove(SelectionIndex, SelectionCount);
            textBox1.SelectionStart = SelectionIndex;
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileDirSelector_ButtonSideChanged/*" />
        private void FileDirSelector_ButtonSideChanged(object sender, EventArgs e)
        {
            AnchorStyles a = AnchorStyles.Top | AnchorStyles.Right;
            AnchorStyles b = AnchorStyles.Top | AnchorStyles.Left;

            this.SuspendLayout();
            switch (m_ButtonSide)
            {
                case ButtonSide.Right:
                    {
                        //move textbox and button over
                        this.button1.Location = new Point(textBox1.Width, -1);
                        this.textBox1.Location = new Point(0, 0);

                        // reset textbox and button anchor points
                        button1.Anchor = a;
                        Refresh();
                    }
                    break;

                case ButtonSide.Left:
                    {
                        //move textbox and button over
                        Point old = button1.Location;
                        button1.Location = new Point(0, -1);
                        textBox1.Location = new Point(21, 0);

                        // reset textbox and button anchor points
                        button1.Anchor = b;
                        Refresh();
                    }
                    break;
            }
            ResumeLayout();
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/M_OpenFileDialog_HelpRequest/*" />
        private void M_OpenFileDialog_HelpRequest(object sender, EventArgs e)
        {
            OnHelpRequest(e);
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/OnButtonSideChanged/*" />
        private void OnButtonSideChanged(EventArgs e)
        {
            if (ButtonSideChanged != null)
                ButtonSideChanged(this, e);
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/OnHelpRequest/*" />
        private void OnHelpRequest(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventHelpRequest];
            if (handler != null) handler(this, e);
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Paste_Click/*" />
        private void Paste_Click(object sender, EventArgs e)
        {
            if (m_FileDirDialog == FileDirDialogType.OpenFileDialog)
            {
                m_OpenFileDialog.FileName = Clipboard.GetText();
                textBox1.Text = ShortenPath(Clipboard.GetText(), TextBoxVisiableChars());
            }
            else
            {
                m_FolderBrowserDialog.SelectedPath = Clipboard.GetText();
                textBox1.Text = ShortenPath(Clipboard.GetText(), TextBoxVisiableChars());
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/SelectAll_Click/*" />
        private void SelectAll_Click(object sender, EventArgs e)
        {
            if (m_FileDirDialog == FileDirDialogType.OpenFileDialog)
            {
                Clipboard.SetText(m_OpenFileDialog.FileName);
            }
            else
            {
                Clipboard.SetText(m_FolderBrowserDialog.SelectedPath);
            }

            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/TextBoxVisiableChars/*" />
        private int TextBoxVisiableChars()
        {
            // this calculation give an approximate number of characters
            Graphics g = Graphics.FromHwnd(this.Handle);
            SizeF sizeF = g.MeasureString("The quick brown fox jumps over the lazy dog", textBox1.Font);
            return (int)(((double)(textBox1.Width - 4)) / sizeF.Width * 43);
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Undo_Click/*" />
        private void Undo_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }
        #endregion Methods

        #region Events

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/HelpRequest/*" />
        public event EventHandler HelpRequest
        {
            add
            {
                Events.AddHandler(EventHelpRequest, value);
            }
            remove
            {
                Events.RemoveHandler(EventHelpRequest, value);
            }
        }

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/ButtonSideChanged/*" />
        private event EventHandler ButtonSideChanged;
        #endregion Events
   
    }

    /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileDirSelectorlDesigner/*" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    internal class FileDirSelectorlDesigner : ControlDesigner
    {
        #region Constructors

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/FileDirSelectorlDesignerConstructor/*" />
        private FileDirSelectorlDesigner()
        {
            base.AutoResizeHandles = true;           
        }

        #endregion Constructors

        #region Properties

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/SelectionRules/*" />
        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable;
            }
        }

        protected override bool EnableDragRect
        {
            get { return true; }
        }

        #endregion Properties
    }

    /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/enumButtonSide/*" />
    public enum ButtonSide
    {
        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Right/*" />
        Right = 0,

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/Left/*" />
        Left = 1
    }

    /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/enumFileDirDialogType/*" />
    public enum FileDirDialogType
    {
        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/enumItemOPENFILEDIALOG/*" />
        OpenFileDialog = 0,

        /// <include file = "..\ClassDocs\CodeDocElements.xml" path="Comments/enumItemFOLDERBROWSERDIALOG/*" />
        FolderBrowserDialog = 1
    }
}