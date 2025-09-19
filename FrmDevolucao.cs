using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProjetoFarmacia
{
    public partial class FrmDevolucao : Form
    {
        Thread ntvoltar;
        public FrmDevolucao()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            ntvoltar = new Thread(NovoFrmInicio);
            ntvoltar.SetApartmentState(ApartmentState.STA);
            ntvoltar.Start();
        }

        private void NovoFrmInicio()
        {
            Application.Run(new FrmInicio());
        }
    }
}
