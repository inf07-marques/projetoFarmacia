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
using MySql.Data.MySqlClient;

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
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = "INSERT INTO funcionario (nome, email, cpf, senha) VALUES (@nome, @email, @cpf, @senha)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@cpf", mskCpf.Text);
                        cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cadastro realizado com sucesso!");
                this.Close();

                ntfinalizarcadastro = new Thread(NovoFrmLogin);
                ntfinalizarcadastro.SetApartmentState(ApartmentState.STA);
                ntfinalizarcadastro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message);
            }
        }
    }
}
