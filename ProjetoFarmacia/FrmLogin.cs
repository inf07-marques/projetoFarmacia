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
    public partial class FrmLogin : Form
    {
        Thread ntEsqueceuSenha, ntentrar, ntproduto, ntpedidos, ntrelatorios, ntdelivery, ntestoque, ntcadastrese;

        private void btnEsqueceuSenha_Click(object sender, EventArgs e)
        {
            this.Close();
            ntEsqueceuSenha = new Thread(NovoFrmRedefinirSenha);
            ntEsqueceuSenha.SetApartmentState(ApartmentState.STA);
            ntEsqueceuSenha.Start();
        }

        private void NovoFrmRedefinirSenha()
        {
            Application.Run(new FrmRedefinirSenha());
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            this.Close();
            ntentrar = new Thread(NovoFrmInicio);
            ntentrar.SetApartmentState(ApartmentState.STA);
            ntentrar.Start();
        }

        private void NovoFrmInicio()
        {
            Application.Run(new FrmInicio());
        }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCadastrese_Click(object sender, EventArgs e)
        {
            this.Close();
            ntcadastrese = new Thread(NovoFrmCadastro);
            ntcadastrese.SetApartmentState(ApartmentState.STA);
            ntcadastrese .Start();

        }

        private void NovoFrmCadastro()
        {
            Application.Run(new FrmCadastro());
        }
    }
}
