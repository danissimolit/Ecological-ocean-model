using OceanSimulation;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OceanWinForms
{
    public partial class OceanForm : Form
    {
        #region Fields

        private readonly WinFormOutput output;
        private readonly Ocean _owner;
        private readonly LoadForm mainForm;
        public Button[,] buttons { get; set; }

        #endregion

        #region Constructor

        public OceanForm(LoadForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            output = new WinFormOutput(this);
            _owner = new Ocean(output);

            _owner.CopyOceanDataFrom(mainForm.mainOcean);

            InitOceanTabel();
            InitSpeedComboBox();

            Width = oceanTable.Width + Constants.oceanFormWidthAddition;
            Height = oceanTable.Height + Constants.oceanFormHeightAddition;

            _owner.AddCells();
            output.FirstPrint(_owner);
        }

        #endregion

        #region Control Events

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start!")
            {
                timerIteration.Start();
                btnStart.Text = "Pause";
            }
            else if (btnStart.Text == "Pause")
            {
                timerIteration.Stop();
                btnStart.Text = "Resume";
            }
            else if (btnStart.Text == "Resume")
            {
                timerIteration.Start();
                btnStart.Text = "Pause";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timerIteration.Stop();
            btnStart.Text = "Start!";
            btnStart.Enabled = true;

            ResetOcean();
        }

        private void cmbboxSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerIteration.Interval = Constants.speedComboBoxValue[cmbboxSpeed.SelectedIndex];
        }

        private void chkbxOptimizer_CheckedChanged(object sender, EventArgs e)
        {
            if (oceanTable.BackColor == Color.Transparent)
            {
                //oceanTable.BackColor = Color.FromArgb(3, 31, 69);
                oceanTable.BackColor = Color.FromArgb(1, 18, 40);
            }
            else oceanTable.BackColor = Color.Transparent;
        }

        private void timerIteration_Tick(object sender, EventArgs e)
        {
            _owner.Iteration();

            if (_owner.numPredators <= 0 || _owner.numPreys <= 0
                || _owner.numIterations <= 0)
            {
                timerIteration.Stop();
                btnStart.Enabled = false;

                //timerIteration.Stop();
                //btnStart.Text = "Start!";
                //btnStart.Enabled = true;

                //ResetOcean();

                //if (btnStart.Text == "Start!")
                //{
                //    timerIteration.Start();
                //    btnStart.Text = "Pause";
                //}

                output.EndOfSimulation(_owner.numPreys,
                    _owner.numPredators, _owner.numIterations);
            }
        }

        private void picboxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OceanForm_Load(object sender, EventArgs e)
        {

        }

        private void OceanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Visible = true;
        }

        private void OceanForm_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        #endregion

        #region Extra Methods

        private void InitOceanTabel()
        {
            oceanTable.RowCount = _owner.rows;
            oceanTable.ColumnCount = _owner.cols;
            buttons = new Button[_owner.rows, _owner.cols];

            for (int i = 0; i < _owner.rows; i++)
            {
                for (int j = 0; j < _owner.cols; j++)
                {
                    buttons[i, j] = CreateNewButton();
                    oceanTable.Controls.Add(buttons[i, j], j, i);
                }
            }

            oceanTable.CellBorderStyle = (TableLayoutPanelCellBorderStyle)_owner.pickedCellBorderStyleIndex;
        }

        private Button CreateNewButton()
        {
            Button button = new Button();

            button.Width = Constants.cellSize[_owner.pickedSize];
            button.Height = Constants.cellSize[_owner.pickedSize];

            button.BackgroundImageLayout = ImageLayout.Zoom;
            button.BackColor = Color.Transparent;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.ForeColor = Color.White;

            return button;
        }

        public void InitSpeedComboBox()
        {
            for (int i = 0; i < Constants.speedComboBoxValue.Length; i++)
            {
                cmbboxSpeed.Items.Add($"x{(double)Constants.defaultSpeed / Constants.speedComboBoxValue[i]}");
            }

            cmbboxSpeed.SelectedIndex = Constants.defaultPickedSpeedIndex;
            cmbboxSpeed.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void ResetOcean()
        {
            _owner.CopyOceanDataFrom(mainForm.mainOcean);
            _owner.AddCells();
            output.FirstPrint(_owner);
        }

        #endregion

        #region Control Visual Events

        private void picboxClose_MouseEnter(object sender, EventArgs e)
        {
            picboxClose.BackgroundImage = Properties.Resources.CloseImgMouseOn;
        }

        private void picboxClose_MouseLeave(object sender, EventArgs e)
        {
            picboxClose.BackgroundImage = Properties.Resources.CloseImg4;
        }

        #endregion

    }
}
