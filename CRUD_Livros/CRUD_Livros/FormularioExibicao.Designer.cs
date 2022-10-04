namespace CRUD_Livros.UserInterface
{
    partial class FormularioExibicao
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.BotaoCadastrar = new System.Windows.Forms.Button();
            this.BotaoEditar = new System.Windows.Forms.Button();
            this.livroBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BotaoDeletar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.livroBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "CRUD Livros";
            // 
            // BotaoCadastrar
            // 
            this.BotaoCadastrar.Location = new System.Drawing.Point(704, 163);
            this.BotaoCadastrar.Name = "BotaoCadastrar";
            this.BotaoCadastrar.Size = new System.Drawing.Size(148, 77);
            this.BotaoCadastrar.TabIndex = 1;
            this.BotaoCadastrar.Text = "CADASTRAR";
            this.BotaoCadastrar.UseVisualStyleBackColor = true;
            this.BotaoCadastrar.Click += new System.EventHandler(this.BotaoCadastrar_Click);
            // 
            // BotaoEditar
            // 
            this.BotaoEditar.Location = new System.Drawing.Point(704, 286);
            this.BotaoEditar.Name = "BotaoEditar";
            this.BotaoEditar.Size = new System.Drawing.Size(148, 73);
            this.BotaoEditar.TabIndex = 2;
            this.BotaoEditar.Text = "EDITAR";
            this.BotaoEditar.UseVisualStyleBackColor = true;
            this.BotaoEditar.Click += new System.EventHandler(this.BotaoEditar_Click);
            // 
            // livroBindingSource
            // 
            this.livroBindingSource.DataSource = typeof(CRUD_Livros.Dominio.RegraDeNegocio.Livro);
            // 
            // BotaoDeletar
            // 
            this.BotaoDeletar.Location = new System.Drawing.Point(704, 402);
            this.BotaoDeletar.Name = "BotaoDeletar";
            this.BotaoDeletar.Size = new System.Drawing.Size(148, 73);
            this.BotaoDeletar.TabIndex = 5;
            this.BotaoDeletar.Text = "DELETAR";
            this.BotaoDeletar.UseVisualStyleBackColor = true;
            this.BotaoDeletar.Click += new System.EventHandler(this.BotaoDeletar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(652, 422);
            this.dataGridView1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 487);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BotaoDeletar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BotaoEditar);
            this.Controls.Add(this.BotaoCadastrar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.livroBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button BotaoCadastrar;
        private Button BotaoEditar;
        private BindingSource livroBindingSource;
        private Button BotaoDeletar;
        public DataGridView dataGridView1;
    }
}