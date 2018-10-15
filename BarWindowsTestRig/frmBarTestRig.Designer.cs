namespace BarWindowsTestRig
{
    partial class frmBarTestRig
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
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.btnCreateInstitution = new System.Windows.Forms.Button();
            this.btnBuyTokens = new System.Windows.Forms.Button();
            this.btnRedeemGoods = new System.Windows.Forms.Button();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.lblIdValue = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnGetTokenBalance = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.radUseSqlite = new System.Windows.Forms.RadioButton();
            this.radUseSqlServer = new System.Windows.Forms.RadioButton();
            this.grpInstitutions = new System.Windows.Forms.GroupBox();
            this.btnGetAllInstitutions = new System.Windows.Forms.Button();
            this.btnGetInstitution = new System.Windows.Forms.Button();
            this.lblInstitutionTel = new System.Windows.Forms.Label();
            this.txtInstitutionTel = new System.Windows.Forms.TextBox();
            this.lblInstitutionEmail = new System.Windows.Forms.Label();
            this.txtInstitutionEmail = new System.Windows.Forms.TextBox();
            this.lblInstitutionPassword = new System.Windows.Forms.Label();
            this.txtInstitutionPassword = new System.Windows.Forms.TextBox();
            this.lblInstitutionName = new System.Windows.Forms.Label();
            this.txtInstitutionName = new System.Windows.Forms.TextBox();
            this.btnFillValues = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendTokens = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.radUseWebApi = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.grpUser.SuspendLayout();
            this.grpInstitutions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(365, 69);
            this.btnCreateUser.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(100, 28);
            this.btnCreateUser.TabIndex = 1;
            this.btnCreateUser.Text = "Create";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // btnCreateInstitution
            // 
            this.btnCreateInstitution.Location = new System.Drawing.Point(359, 75);
            this.btnCreateInstitution.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateInstitution.Name = "btnCreateInstitution";
            this.btnCreateInstitution.Size = new System.Drawing.Size(100, 28);
            this.btnCreateInstitution.TabIndex = 2;
            this.btnCreateInstitution.Text = "Create";
            this.btnCreateInstitution.UseVisualStyleBackColor = true;
            
            // 
            // btnBuyTokens
            // 
            this.btnBuyTokens.Location = new System.Drawing.Point(872, 513);
            this.btnBuyTokens.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuyTokens.Name = "btnBuyTokens";
            this.btnBuyTokens.Size = new System.Drawing.Size(100, 28);
            this.btnBuyTokens.TabIndex = 3;
            this.btnBuyTokens.Text = "Buy Tokens";
            this.btnBuyTokens.UseVisualStyleBackColor = true;
            // 
            // btnRedeemGoods
            // 
            this.btnRedeemGoods.Location = new System.Drawing.Point(888, 629);
            this.btnRedeemGoods.Margin = new System.Windows.Forms.Padding(4);
            this.btnRedeemGoods.Name = "btnRedeemGoods";
            this.btnRedeemGoods.Size = new System.Drawing.Size(100, 28);
            this.btnRedeemGoods.TabIndex = 4;
            this.btnRedeemGoods.Text = "Redeem Goods";
            this.btnRedeemGoods.UseVisualStyleBackColor = true;
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.lblIdValue);
            this.grpUser.Controls.Add(this.lblId);
            this.grpUser.Controls.Add(this.btnLogout);
            this.grpUser.Controls.Add(this.btnGetTokenBalance);
            this.grpUser.Controls.Add(this.label3);
            this.grpUser.Controls.Add(this.btnDeleteUser);
            this.grpUser.Controls.Add(this.txtTelephone);
            this.grpUser.Controls.Add(this.btnUpdate);
            this.grpUser.Controls.Add(this.lblEmail);
            this.grpUser.Controls.Add(this.txtEmail);
            this.grpUser.Controls.Add(this.btnCreateUser);
            this.grpUser.Controls.Add(this.btnLogin);
            this.grpUser.Controls.Add(this.lblPassword);
            this.grpUser.Controls.Add(this.txtPassword);
            this.grpUser.Controls.Add(this.label1);
            this.grpUser.Controls.Add(this.txtUserName);
            this.grpUser.Location = new System.Drawing.Point(33, 84);
            this.grpUser.Margin = new System.Windows.Forms.Padding(4);
            this.grpUser.Name = "grpUser";
            this.grpUser.Padding = new System.Windows.Forms.Padding(4);
            this.grpUser.Size = new System.Drawing.Size(556, 272);
            this.grpUser.TabIndex = 5;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "User";
            // 
            // lblIdValue
            // 
            this.lblIdValue.AutoSize = true;
            this.lblIdValue.Location = new System.Drawing.Point(71, 236);
            this.lblIdValue.Name = "lblIdValue";
            this.lblIdValue.Size = new System.Drawing.Size(19, 17);
            this.lblIdValue.TabIndex = 13;
            this.lblIdValue.Text = "Id";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(32, 236);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(19, 17);
            this.lblId.TabIndex = 12;
            this.lblId.Text = "Id";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(367, 212);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 28);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "Login";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnGetTokenBalance
            // 
            this.btnGetTokenBalance.Location = new System.Drawing.Point(236, 212);
            this.btnGetTokenBalance.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetTokenBalance.Name = "btnGetTokenBalance";
            this.btnGetTokenBalance.Size = new System.Drawing.Size(100, 28);
            this.btnGetTokenBalance.TabIndex = 10;
            this.btnGetTokenBalance.Text = "Balance";
            this.btnGetTokenBalance.UseVisualStyleBackColor = true;

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Telephone";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(367, 143);
            this.btnDeleteUser.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(128, 28);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(140, 145);
            this.txtTelephone.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(195, 22);
            this.txtTelephone.TabIndex = 7;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(365, 105);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(128, 28);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update User";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(21, 117);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 17);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(140, 113);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(195, 22);
            this.txtEmail.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(365, 33);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 28);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(21, 81);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 17);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(140, 78);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(195, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "UserName";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(140, 42);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(195, 22);
            this.txtUserName.TabIndex = 1;
            // 
            // radUseSqlite
            // 
            this.radUseSqlite.AutoSize = true;
            this.radUseSqlite.Checked = true;
            this.radUseSqlite.Location = new System.Drawing.Point(33, 15);
            this.radUseSqlite.Margin = new System.Windows.Forms.Padding(4);
            this.radUseSqlite.Name = "radUseSqlite";
            this.radUseSqlite.Size = new System.Drawing.Size(152, 21);
            this.radUseSqlite.TabIndex = 9;
            this.radUseSqlite.TabStop = true;
            this.radUseSqlite.Text = "Use Sqlite Backend";
            this.radUseSqlite.UseVisualStyleBackColor = true;
            // 
            // radUseSqlServer
            // 
            this.radUseSqlServer.AutoSize = true;
            this.radUseSqlServer.Location = new System.Drawing.Point(33, 43);
            this.radUseSqlServer.Margin = new System.Windows.Forms.Padding(4);
            this.radUseSqlServer.Name = "radUseSqlServer";
            this.radUseSqlServer.Size = new System.Drawing.Size(183, 21);
            this.radUseSqlServer.TabIndex = 10;
            this.radUseSqlServer.Text = "Use Sql Server Backend";
            this.radUseSqlServer.UseVisualStyleBackColor = true;
            // 
            // grpInstitutions
            // 
            this.grpInstitutions.Controls.Add(this.btnGetAllInstitutions);
            this.grpInstitutions.Controls.Add(this.btnGetInstitution);
            this.grpInstitutions.Controls.Add(this.lblInstitutionTel);
            this.grpInstitutions.Controls.Add(this.txtInstitutionTel);
            this.grpInstitutions.Controls.Add(this.lblInstitutionEmail);
            this.grpInstitutions.Controls.Add(this.txtInstitutionEmail);
            this.grpInstitutions.Controls.Add(this.lblInstitutionPassword);
            this.grpInstitutions.Controls.Add(this.btnCreateInstitution);
            this.grpInstitutions.Controls.Add(this.txtInstitutionPassword);
            this.grpInstitutions.Controls.Add(this.lblInstitutionName);
            this.grpInstitutions.Controls.Add(this.txtInstitutionName);
            this.grpInstitutions.Location = new System.Drawing.Point(872, 84);
            this.grpInstitutions.Margin = new System.Windows.Forms.Padding(4);
            this.grpInstitutions.Name = "grpInstitutions";
            this.grpInstitutions.Padding = new System.Windows.Forms.Padding(4);
            this.grpInstitutions.Size = new System.Drawing.Size(504, 240);
            this.grpInstitutions.TabIndex = 11;
            this.grpInstitutions.TabStop = false;
            this.grpInstitutions.Text = "Goods Supplier";
            // 
            // btnGetAllInstitutions
            // 
            this.btnGetAllInstitutions.Location = new System.Drawing.Point(359, 117);
            this.btnGetAllInstitutions.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetAllInstitutions.Name = "btnGetAllInstitutions";
            this.btnGetAllInstitutions.Size = new System.Drawing.Size(100, 28);
            this.btnGetAllInstitutions.TabIndex = 10;
            this.btnGetAllInstitutions.Text = "Gt All";
            this.btnGetAllInstitutions.UseVisualStyleBackColor = true;
            this.btnGetAllInstitutions.Click += new System.EventHandler(this.btnGetAllInstitutions_Click);
            // 
            // btnGetInstitution
            // 
            this.btnGetInstitution.Location = new System.Drawing.Point(359, 39);
            this.btnGetInstitution.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetInstitution.Name = "btnGetInstitution";
            this.btnGetInstitution.Size = new System.Drawing.Size(100, 28);
            this.btnGetInstitution.TabIndex = 9;
            this.btnGetInstitution.Text = "Login";
            this.btnGetInstitution.UseVisualStyleBackColor = true;
            
            // 
            // lblInstitutionTel
            // 
            this.lblInstitutionTel.AutoSize = true;
            this.lblInstitutionTel.Location = new System.Drawing.Point(21, 149);
            this.lblInstitutionTel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstitutionTel.Name = "lblInstitutionTel";
            this.lblInstitutionTel.Size = new System.Drawing.Size(76, 17);
            this.lblInstitutionTel.TabIndex = 8;
            this.lblInstitutionTel.Text = "Telephone";
            // 
            // txtInstitutionTel
            // 
            this.txtInstitutionTel.Location = new System.Drawing.Point(140, 145);
            this.txtInstitutionTel.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstitutionTel.Name = "txtInstitutionTel";
            this.txtInstitutionTel.Size = new System.Drawing.Size(195, 22);
            this.txtInstitutionTel.TabIndex = 7;
            // 
            // lblInstitutionEmail
            // 
            this.lblInstitutionEmail.AutoSize = true;
            this.lblInstitutionEmail.Location = new System.Drawing.Point(21, 117);
            this.lblInstitutionEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstitutionEmail.Name = "lblInstitutionEmail";
            this.lblInstitutionEmail.Size = new System.Drawing.Size(42, 17);
            this.lblInstitutionEmail.TabIndex = 6;
            this.lblInstitutionEmail.Text = "Email";
            // 
            // txtInstitutionEmail
            // 
            this.txtInstitutionEmail.Location = new System.Drawing.Point(140, 113);
            this.txtInstitutionEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstitutionEmail.Name = "txtInstitutionEmail";
            this.txtInstitutionEmail.Size = new System.Drawing.Size(195, 22);
            this.txtInstitutionEmail.TabIndex = 5;
            // 
            // lblInstitutionPassword
            // 
            this.lblInstitutionPassword.AutoSize = true;
            this.lblInstitutionPassword.Location = new System.Drawing.Point(21, 81);
            this.lblInstitutionPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstitutionPassword.Name = "lblInstitutionPassword";
            this.lblInstitutionPassword.Size = new System.Drawing.Size(69, 17);
            this.lblInstitutionPassword.TabIndex = 4;
            this.lblInstitutionPassword.Text = "Password";
            // 
            // txtInstitutionPassword
            // 
            this.txtInstitutionPassword.Location = new System.Drawing.Point(140, 78);
            this.txtInstitutionPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstitutionPassword.Name = "txtInstitutionPassword";
            this.txtInstitutionPassword.Size = new System.Drawing.Size(195, 22);
            this.txtInstitutionPassword.TabIndex = 3;
            // 
            // lblInstitutionName
            // 
            this.lblInstitutionName.AutoSize = true;
            this.lblInstitutionName.Location = new System.Drawing.Point(21, 46);
            this.lblInstitutionName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInstitutionName.Name = "lblInstitutionName";
            this.lblInstitutionName.Size = new System.Drawing.Size(109, 17);
            this.lblInstitutionName.TabIndex = 2;
            this.lblInstitutionName.Text = "Institution Name";
            // 
            // txtInstitutionName
            // 
            this.txtInstitutionName.Location = new System.Drawing.Point(140, 42);
            this.txtInstitutionName.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstitutionName.Name = "txtInstitutionName";
            this.txtInstitutionName.Size = new System.Drawing.Size(195, 22);
            this.txtInstitutionName.TabIndex = 1;
            // 
            // btnFillValues
            // 
            this.btnFillValues.Location = new System.Drawing.Point(909, 15);
            this.btnFillValues.Margin = new System.Windows.Forms.Padding(4);
            this.btnFillValues.Name = "btnFillValues";
            this.btnFillValues.Size = new System.Drawing.Size(100, 28);
            this.btnFillValues.TabIndex = 12;
            this.btnFillValues.Text = "Pre-fill Values";
            this.btnFillValues.UseVisualStyleBackColor = true;
            this.btnFillValues.Click += new System.EventHandler(this.btnFillValues_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendTokens);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Location = new System.Drawing.Point(33, 417);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(368, 240);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buy Tokens";
            // 
            // btnSendTokens
            // 
            this.btnSendTokens.Location = new System.Drawing.Point(15, 193);
            this.btnSendTokens.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendTokens.Name = "btnSendTokens";
            this.btnSendTokens.Size = new System.Drawing.Size(144, 28);
            this.btnSendTokens.TabIndex = 9;
            this.btnSendTokens.Text = "Send Tokens";
            this.btnSendTokens.UseVisualStyleBackColor = true;
            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 149);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Telephone";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 145);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(195, 22);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(140, 113);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(195, 22);
            this.textBox2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Password";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(192, 193);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create Token";
            this.button1.UseVisualStyleBackColor = true;
            
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(140, 78);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(195, 22);
            this.textBox3.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Institution Name";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(140, 42);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(195, 22);
            this.textBox4.TabIndex = 1;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(436, 604);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(377, 88);
            this.txtOutput.TabIndex = 6;
            // 
            // radUseWebApi
            // 
            this.radUseWebApi.AutoSize = true;
            this.radUseWebApi.Location = new System.Drawing.Point(244, 15);
            this.radUseWebApi.Margin = new System.Windows.Forms.Padding(4);
            this.radUseWebApi.Name = "radUseWebApi";
            this.radUseWebApi.Size = new System.Drawing.Size(107, 21);
            this.radUseWebApi.TabIndex = 15;
            this.radUseWebApi.Text = "Use WebApi";
            this.radUseWebApi.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(524, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 22);
            this.button2.TabIndex = 16;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(738, 417);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(738, 459);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            
            // 
            // frmBarTestRig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 708);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.radUseWebApi);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFillValues);
            this.Controls.Add(this.grpInstitutions);
            this.Controls.Add(this.radUseSqlite);
            this.Controls.Add(this.radUseSqlServer);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.btnRedeemGoods);
            this.Controls.Add(this.btnBuyTokens);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmBarTestRig";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmBarTestRig_Load);
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.grpInstitutions.ResumeLayout(false);
            this.grpInstitutions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnCreateInstitution;
        private System.Windows.Forms.Button btnBuyTokens;
        private System.Windows.Forms.Button btnRedeemGoods;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.RadioButton radUseSqlite;
        private System.Windows.Forms.RadioButton radUseSqlServer;
        private System.Windows.Forms.GroupBox grpInstitutions;
        private System.Windows.Forms.Label lblInstitutionTel;
        private System.Windows.Forms.TextBox txtInstitutionTel;
        private System.Windows.Forms.Label lblInstitutionEmail;
        private System.Windows.Forms.TextBox txtInstitutionEmail;
        private System.Windows.Forms.Label lblInstitutionPassword;
        private System.Windows.Forms.TextBox txtInstitutionPassword;
        private System.Windows.Forms.Label lblInstitutionName;
        private System.Windows.Forms.TextBox txtInstitutionName;
        private System.Windows.Forms.Button btnFillValues;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.RadioButton radUseWebApi;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnGetInstitution;
        private System.Windows.Forms.Button btnGetTokenBalance;
        private System.Windows.Forms.Button btnSendTokens;
        private System.Windows.Forms.Button btnGetAllInstitutions;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblIdValue;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

