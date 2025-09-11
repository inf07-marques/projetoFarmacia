using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProjetoFarmacia
{
    public partial class FrmEstoque : Form
    {
        Thread ntVoltar;
        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            ntVoltar = new Thread(NovoFrmInicio);
            ntVoltar.SetApartmentState(ApartmentState.STA);
            ntVoltar.Start();
        }

        private void NovoFrmInicio()
        {
            Application.Run(new FrmInicio());
        }
    }
}
