using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OceanSimulation;

namespace OceanWinForms
{
    public partial class LoadForm : Form
    {
        #region Fields

        public Ocean mainOcean { get; set; }
        Coordinate formPosition { get; set; }

        #endregion

        #region Constructor

        public LoadForm()
        {
            InitializeComponent();
            mainOcean = new Ocean();
            mainOcean.SetDefaultOcean();
        }

        #endregion

        #region Control Events

        private void picboxStart_Click(object sender, EventArgs e)
        {
            //Visible = false;
            OceanForm oceanForm = new OceanForm(this);
            oceanForm.Show();
        }

        private void picboxSettings_Click(object sender, EventArgs e)
        {
            formPosition = new Coordinate(Left, Top);
            SettingsForm settingsForm = new SettingsForm(formPosition, mainOcean);

            settingsForm.ShowDialog();
        }

        private void picboxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadForm_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void lblCaption_MouseDown(object sender, MouseEventArgs e)
        {
            lblCaption.Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        #endregion

        #region Control Visual Events

        private void picboxSettings_MouseEnter(object sender, EventArgs e)
        {
            picboxSettings.BackgroundImage = Properties.Resources.SettingsImgMouseOn;
        }

        private void picboxSettings_MouseLeave(object sender, EventArgs e)
        {
            picboxSettings.BackgroundImage = Properties.Resources.SettingsImg2;
        }

        private void picboxStart_MouseEnter(object sender, EventArgs e)
        {
            picboxStart.BackgroundImage = Properties.Resources.StartImgButtonMouseOn;
        }

        private void picboxStart_MouseLeave(object sender, EventArgs e)
        {
            picboxStart.BackgroundImage = Properties.Resources.StartImgButton4;
        }

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
