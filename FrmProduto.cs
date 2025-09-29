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
    public partial class FrmProduto : Form
    {
        Thread ntVoltar, ntFinalizarCadastroProduto;
        public FrmProduto()
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

        private void btnFinalizarCadastroProduto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text) ||
               string.IsNullOrWhiteSpace(mskPrecoProduto.Text) ||
               string.IsNullOrWhiteSpace(mskValProduto.Text) ||
               string.IsNullOrWhiteSpace(txtQtdProduto.Text))
            {
                MessageBox.Show("Erro ao cadastrar");
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = "INSERT INTO produto (nome, preco, validade, qtdestoque) VALUES (@nome, @preco, @validade, @qtdestoque)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNomeProduto.Text);

                        decimal preco;
                        if (!decimal.TryParse(mskPrecoProduto.Text, System.Globalization.NumberStyles.Number, new System.Globalization.CultureInfo("pt-BR"), out preco))
                        {
                            MessageBox.Show("Preço inválido! Digite apenas números e vírgula, exemplo: 10,50");
                            return;
                        }

                        cmd.Parameters.AddWithValue("@preco", preco);
                        cmd.Parameters.AddWithValue("@validade", mskValProduto.Text);
                        cmd.Parameters.AddWithValue("@qtdestoque", txtQtdProduto.Text);
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

                ntFinalizarCadastroProduto = new Thread(NovoFrmInicio);
                ntFinalizarCadastroProduto.SetApartmentState(ApartmentState.STA);
                ntFinalizarCadastroProduto.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message);
            }
        }
    }
}
