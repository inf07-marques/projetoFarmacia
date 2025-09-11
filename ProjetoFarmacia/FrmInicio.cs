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
    public partial class FrmInicio : Form
    {
        Thread ntSair, ntProdutos, ntestoque, ntpedidos, ntdelivery, ntrelatorios, ntdevolucao;
        public FrmInicio()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            ntSair = new Thread(NovoFrmLogin);
            ntSair.SetApartmentState(ApartmentState.STA);
            ntSair.Start();
        }

        private void NovoFrmLogin()
        {
            Application.Run(new FrmLogin());
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            this.Close();
            ntProdutos = new Thread(NovoFrmProduto);
            ntProdutos.SetApartmentState (ApartmentState.STA);
            ntProdutos.Start();
        }

        private void btnEstoque_Click(object sender, EventArgs e)
        {
            this .Close();
            ntestoque = new Thread(NovoFrmEstoque);
            ntestoque.SetApartmentState(ApartmentState .STA);
            ntestoque.Start();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            this.Close ();
            ntpedidos = new Thread(NovoFrmPedidos);
            ntpedidos.SetApartmentState(ApartmentState.STA);
            ntpedidos.Start();
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            this.Close();
            ntdelivery = new Thread(NovoFrmDelivery);
            ntdelivery.SetApartmentState(ApartmentState.STA);
            ntdelivery.Start();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            this.Close();
            ntrelatorios = new Thread(NovoFrmRelatorios);
            ntrelatorios.SetApartmentState(ApartmentState.STA) ;
            ntrelatorios.Start();
        }

        private void btnDevolucao_Click(object sender, EventArgs e)
        {
            this .Close();
            ntdevolucao = new Thread(NovoFrmDevolucao);
            ntdevolucao.SetApartmentState (ApartmentState.STA) ;
            ntdevolucao.Start();
        }

        private void NovoFrmDevolucao()
        {
            Application.Run(new FrmDevolucao());
        }

        private void NovoFrmRelatorios()
        {
            Application.Run(new FrmRelatorios());
        }

        private void NovoFrmDelivery()
        {
            Application.Run(new FrmDelivery());
        }

        private void NovoFrmPedidos()
        {
            Application.Run (new FrmPedidos());
        }

        private void NovoFrmEstoque()
        {
            Application.Run (new FrmEstoque());
        }

        private void NovoFrmProduto()
        {
            Application.Run(new FrmProduto());
        }
    }
}
