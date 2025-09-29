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
    public partial class FrmClientes : Form
    {
        Thread ntVoltar, ntfinalizarcadastro;
        public FrmClientes()
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

        private void btnFinalizarCadastroCliente_Click(object sender, EventArgs e)
        {
            // Validação dos campos obrigatórios
            if (string.IsNullOrWhiteSpace(txtNomeCliente.Text) ||
                string.IsNullOrWhiteSpace(mskCepCliente.Text) ||
                string.IsNullOrWhiteSpace(mskCpfCliente.Text) ||
                string.IsNullOrWhiteSpace(txtEmailCliente.Text) ||
                string.IsNullOrWhiteSpace(txtEnderecoCliente.Text) ||
                string.IsNullOrWhiteSpace(mskTelCliente.Text) ||
                string.IsNullOrWhiteSpace(txtBairroCliente.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCliente.Text))
            {
                MessageBox.Show("Erro ao cadastrar");
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = "INSERT INTO cliente (nome, cep, cpf, email, endereco, telefone, bairro, numero) VALUES (@nome, @cep, @cpf, @email, @endereco, @telefone, @bairro, @numero)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNomeCliente.Text);
                        cmd.Parameters.AddWithValue("@cep", mskCepCliente.Text);
                        cmd.Parameters.AddWithValue("@cpf", mskCpfCliente.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmailCliente.Text);
                        cmd.Parameters.AddWithValue("@endereco", txtEnderecoCliente.Text);
                        cmd.Parameters.AddWithValue("@telefone", mskTelCliente.Text);
                        cmd.Parameters.AddWithValue("@bairro", txtBairroCliente.Text);
                        cmd.Parameters.AddWithValue("@numero", txtNumeroCliente.Text);
                        int result = cmd.ExecuteNonQuery();

                        if (result == 0)
                        {
                            MessageBox.Show("Erro ao cadastrar");
                            return;
                        }
                    }
                }

                MessageBox.Show("Cadastro realizado com sucesso!");
                this.Close();

                ntfinalizarcadastro = new Thread(NovoFrmInicio);
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