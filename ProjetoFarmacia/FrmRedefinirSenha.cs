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
    
   
    public partial class FrmRedefinirSenha : Form
    {
        Thread ntconfirmar, ntvoltar;
        public FrmRedefinirSenha()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.Close();
            ntconfirmar = new Thread(NovoFrmLogin);
            ntconfirmar.SetApartmentState(ApartmentState.STA);
            ntconfirmar.Start();
        }

        private void NovoFrmLogin()
        {
            Application.Run(new FrmLogin());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            ntvoltar = new Thread(NovoFrmLogin);
            ntvoltar.SetApartmentState (ApartmentState.STA);
            ntvoltar.Start();
        }
    }

}
