using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace ProjetoFarmacia
{
   

    public partial class FrmPedidos : Form
    {
        #region Campos
        Thread ntVoltar;
        private List<PedidoItem> itensPedido = new List<PedidoItem>();
        #endregion

        #region Construtor
        public FrmPedidos()
        {
            InitializeComponent();
        }
        #endregion

        #region Navegação
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
        #endregion

        #region Carregamento de Produtos
        private void FrmPedidos_Load(object sender, EventArgs e)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT id, nome FROM produto";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBoxProdutos.Items.Add(new
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome")
                        });
                    }
                }
            }

            comboBoxProdutos.DisplayMember = "Nome";
            comboBoxProdutos.ValueMember = "Id";
        }
        #endregion

        #region Adicionar Produto ao Pedido
        private void btnAdicionarPedido_Click(object sender, EventArgs e)
        {
            if (comboBoxProdutos.SelectedItem == null || string.IsNullOrWhiteSpace(txtQuantidade.Text))
            {
                MessageBox.Show("Selecione um produto e informe a quantidade.");
                return;
            }

            int quantidade;
            if (!int.TryParse(txtQuantidade.Text, out quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            dynamic produto = comboBoxProdutos.SelectedItem;
            itensPedido.Add(new PedidoItem
            {
                ProdutoId = produto.Id,
                Nome = produto.Nome,
                Quantidade = quantidade
            });

            dgvPedidos.DataSource = null;
            dgvPedidos.DataSource = itensPedido;
        }
        #endregion

        #region Finalizar Pedido
        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                foreach (var item in itensPedido)
                {
                    // Atualiza o estoque do produto
                    string sql = "UPDATE produto SET qtdestoque = qtdestoque - @quantidade WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                        cmd.Parameters.AddWithValue("@id", item.ProdutoId);
                        cmd.ExecuteNonQuery();
                    }
                    // Aqui você pode inserir o pedido e os itens em tabelas específicas
                }
            }
            MessageBox.Show("Pedido finalizado!");
            itensPedido.Clear();
            dgvPedidos.DataSource = null;
        }
        #endregion
        #region Classe Auxiliar
        public class PedidoItem
        {
            public int ProdutoId { get; set; }
            public string Nome { get; set; }
            public int Quantidade { get; set; }
        }
        #endregion
    }
}
