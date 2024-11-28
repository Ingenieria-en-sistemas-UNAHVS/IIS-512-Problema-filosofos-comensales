namespace ProblemaFilosofosGrafico
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.imgMesa = new System.Windows.Forms.PictureBox();
            this.imgPlato1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgMesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPlato1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese la cantidad de filósofos";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(341, 38);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(183, 20);
            this.txtCantidad.TabIndex = 1;
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(544, 36);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(99, 22);
            this.btnEjecutar.TabIndex = 2;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // imgMesa
            // 
            this.imgMesa.BackColor = System.Drawing.Color.Transparent;
            this.imgMesa.Image = ((System.Drawing.Image)(resources.GetObject("imgMesa.Image")));
            this.imgMesa.Location = new System.Drawing.Point(182, 133);
            this.imgMesa.Name = "imgMesa";
            this.imgMesa.Size = new System.Drawing.Size(444, 449);
            this.imgMesa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgMesa.TabIndex = 3;
            this.imgMesa.TabStop = false;
            // 
            // imgPlato1
            // 
            this.imgPlato1.BackColor = System.Drawing.Color.Transparent;
            this.imgPlato1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgPlato1.Image = ((System.Drawing.Image)(resources.GetObject("imgPlato1.Image")));
            this.imgPlato1.Location = new System.Drawing.Point(366, 164);
            this.imgPlato1.Name = "imgPlato1";
            this.imgPlato1.Size = new System.Drawing.Size(78, 68);
            this.imgPlato1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPlato1.TabIndex = 4;
            this.imgPlato1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 645);
            this.Controls.Add(this.imgPlato1);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgMesa);
            this.Name = "Form1";
            this.Text = "Problema de los filósofos comensales";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgMesa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPlato1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.PictureBox imgMesa;
        private System.Windows.Forms.PictureBox imgPlato1;
    }
}

