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
    public partial class FrmCadastro : Form
    {
        Thread ntvoltar, ntfinalizarcadastro;
        public FrmCadastro()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            ntvoltar = new Thread(NovoFrmLogin);
            ntvoltar.SetApartmentState(ApartmentState.STA);
            ntvoltar.Start();
        }

        private void NovoFrmLogin()
        {
            Application.Run(new FrmLogin());
        }

        private void btnFinalizarCadastro_Click(object sender, EventArgs e)
        {
            this.Close();
            ntfinalizarcadastro = new Thread(NovoFrmLogin);
            ntfinalizarcadastro.SetApartmentState(ApartmentState.STA);
            ntfinalizarcadastro.Start();
        }
    }
}
