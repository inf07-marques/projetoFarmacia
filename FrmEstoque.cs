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
using MySql.Data.MySqlClient;

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

        private void FrmEstoque_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT id, nome, preco, validade, qtdestoque FROM produto";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewProdutos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private void dataGridViewProdutos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var row = dataGridViewProdutos.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["id"].Value);
                string nome = row.Cells["nome"].Value.ToString();
                decimal preco = Convert.ToDecimal(row.Cells["preco"].Value);
                string validade = row.Cells["validade"].Value.ToString();
                int qtdestoque = Convert.ToInt32(row.Cells["qtdestoque"].Value);

                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE produto SET nome=@nome, preco=@preco, validade=@validade, qtdestoque=@qtdestoque WHERE id=@id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@preco", preco);
                        cmd.Parameters.AddWithValue("@validade", validade);
                        cmd.Parameters.AddWithValue("@qtdestoque", qtdestoque);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar produto: " + ex.Message);
            }
        }
    }
}
