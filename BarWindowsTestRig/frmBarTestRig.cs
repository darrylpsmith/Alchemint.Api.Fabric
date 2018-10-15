using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alchemint.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Reflection;
using Sam.DataModel;
using Alchemint.Client.JsonAccess;

namespace BarWindowsTestRig
{

    public partial class frmBarTestRig : Form
    {

        //FabricJsonAccess _fabricAccess = new FabricJsonAccess(
        //    @"https://localhost:44329/api/",
        //    "33c35730-2deb-44ae-9a16-1dec27960052");

        FabricJsonAccess _fabricAccess = new FabricJsonAccess(
            @"https://localhost:5001/api/",
            "33c35730-2deb-44ae-9a16-1dec27960052");

        

        public frmBarTestRig()
        {
            InitializeComponent();
        }



        private void frmBarTestRig_Load(object sender, EventArgs e)
        {

            long val = 100000;
            int val2 = 0;


            //if (val.GetType().Name.StartsWith("Int"))
            //{
            //    if (val.GetType().Name != val.GetType().Name)
            //    {
            //        if (val < Int32.MaxValue && val > Int32.MinValue)

            //    }
            //}
            //if (val < Int32.MaxValue && val > Int32.MinValue)
            //{
            //    val2 = (Int32)val;
            //}

            btnFillValues_Click(sender, e);

            //this.btnLogout.Click += new System.EventHandler(btnLogout_Click);
            //this.btnLogout.Click += btnLogout_Click;
            LogoutClicker loggerOuterFunction = Logout;

            //this.btnLogout.Click +=  (s,a) => {Logout(); };
            //this.btnLogout.Click += (s, a) => { loggerOuterFunction(); };
            this.btnLogout.Click += this.Logout;

        }


        delegate void LogoutClicker(object s, EventArgs e);

        private void Logout(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Clicked");
        }

        private I CreateInstance<I>() where I : class
        {
            string assemblyPath = Environment.CurrentDirectory + "\\BarClasses.dll";

            Assembly assembly;

            assembly = Assembly.LoadFrom(assemblyPath);
            Type type = assembly.GetType("Alchemint.Bar.Party");
            return Activator.CreateInstance(type) as I;
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {


            try
            {
                Party entity = new Party { Id = Guid.NewGuid().ToString(), Name = txtUserName.Text, IdentificationNumber = Guid.NewGuid().ToString(), Type = 0 };
                Party entityCreated = (Party)AsyncHelpers.RunSync<object>(() => _fabricAccess.CreateEntity(entity));
                //LegalContract entity = new LegalContract { Id = Guid.NewGuid().ToString(), Name = txtUserName.Text };
                //LegalContract entityCreated = (LegalContract)AsyncHelpers.RunSync<object>(() => _fabricAccess.CreateEntity(entity));


                string entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
                    txtOutput.Text = entityJson;

            }
            catch (Exception)
            {
                MessageBox.Show(((ExclusiveSynchronizationContext)SynchronizationContext.Current).InnerException.Message);
            }



        }


        private void btnFillValues_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "Test User";
            txtPassword.Text = "TestPassword";
            txtEmail.Text = "TestEmailXXXXXXX.com";
            txtTelephone.Text = "082-555-555555";

            txtInstitutionName.Text = "Test User";
            txtInstitutionPassword.Text = "TestPassword";
            txtInstitutionEmail.Text = "TestEmailXXXXXXX.com";
            txtInstitutionTel.Text = "082-555-555555";

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {

            //Party entity = new Party { Name = txtUserName.Text, Password = txtPassword.Text};
            //FabricJsonAccess jsonAccess = new FabricJsonAccess("33c35730-2deb-44ae-9a16-1dec27960052");
            //Party entityCreated = (Party)AsyncHelpers.RunSync<object>(() => jsonAccess.GetEntity(entity, jsonAccess.BuildFilterList("UserName,Password")));

            //string entityJson = JsonConvert.SerializeObject(entityCreated, Formatting.Indented);
            //txtOutput.Text = entityJson;

            //lblIdValue.Text = entityCreated.Id;


        }






        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string Id = lblIdValue.Text;
            Party entity = new Party { Id = Id, Name = txtUserName.Text};
            Party entityCreated = (Party)AsyncHelpers.RunSync<object>(() => _fabricAccess.UpdateEntity (entity));

            string entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
            txtOutput.Text = entityJson;
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string Id = lblIdValue.Text;
            Party entity = new Party {Id = Id, Name = txtUserName.Text};
            Party entityCreated = (Party)AsyncHelpers.RunSync<object>(() => _fabricAccess.DeleteEntity(entity));

            string entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
            txtOutput.Text = entityJson;


        }

        private void btnGetAllInstitutions_Click(object sender, EventArgs e)
        {

                //BarInstitution entity = new BarInstitution  {};
                //FabricJsonAccess jsonAccess = new FabricJsonAccess("33c35730-2deb-44ae-9a16-1dec27960052");
                //List<object> entityCreated = (List<object>)AsyncHelpers.RunSync<List<object>>(() => jsonAccess.GetEntities(entity, null));
                //List<BarInstitution> institutions = entityCreated.Cast<BarInstitution>().ToList();
                //string entityJson = JsonConvert.SerializeObject(institutions, Formatting.Indented);
                //txtOutput.Text = entityJson;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Patient entity = new Patient { IdNumber = "p"};
            //Patient entityCreated = (Patient)AsyncHelpers.RunSync<object>(() => jsonAccess.GetEntity(entity, jsonAccess.BuildFilterList("IdNumber")));

            //string entityJson = JsonConvert.SerializeObject(entityCreated, Formatting.Indented);
            //txtOutput.Text = entityJson;

        }



        //public void CreateApiKey()
        //{
        //    ApiKey entity = new BarToken { Id = Guid.NewGuid().ToString(), IssueTime = DateTime.UtcNow, OriginatorWalletAddress = Guid.NewGuid().ToString(), CurrentWallet = Guid.NewGuid().ToString() };
        //    FabricJsonAccess jsonAccess = new FabricJsonAccess("33c35730-2deb-44ae-9a16-1dec27960052");
        //    BarToken entityCreated = (BarToken)AsyncHelpers.RunSync<object>(() => jsonAccess.CreateEntity(entity));

        //    string entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
        //    txtOutput.Text = entityJson;
        //}
    }
}
