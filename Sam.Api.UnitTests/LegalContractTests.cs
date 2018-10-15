using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alchemint.Client.JsonAccess;
using Sam.DataModel;
using System.Threading.Tasks;
using System;

namespace Sam.Api.UnitTests
{
    [TestClass]
    public class LegalContractTests
    {


        [DataTestMethod]
        
        [DataRow("Contract 12/34")]
        [DataRow("Contract 56/78")]
        public void CreateLegalContract(string Name)
        {
            Sam.DataModel.LegalContract entity = new Sam.DataModel.LegalContract { Name = Name};
            Sam.DataModel.LegalContract entityCreated = (Sam.DataModel.LegalContract)StaticFunctions.FabricAccess.CreateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("id1", "Contract AB/CD")]
        [DataRow("id2", "Contract EF/GH")]
        public void CreateLegalContractWithId(string Id, string Name)
        {
            Sam.DataModel.LegalContract entity = new Sam.DataModel.LegalContract {Id = Id, Name = Name };
            Sam.DataModel.LegalContract entityCreated = (Sam.DataModel.LegalContract)StaticFunctions.FabricAccess.CreateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("Contract 12/34")]
        [DataRow("Contract 56/78")]
        public void GetLegalContract(string Name)
        {
            LegalContract entity = new LegalContract { Name = Name };
            LegalContract entityReturned = (LegalContract)StaticFunctions.FabricAccess.GetEntity(entity, StaticFunctions.FabricAccess.BuildFilterList("Name")).Result;
            Assert.IsTrue(entityReturned != null);
        }

        [DataTestMethod]
        [DataRow("id1")]
        [DataRow("id2")]
        public void GetLegalContractById(string Id)
        {
            LegalContract entity = new LegalContract { Id = Id };
            LegalContract entityReturned = (LegalContract)StaticFunctions.FabricAccess.GetEntity(entity, StaticFunctions.FabricAccess.BuildFilterList("Id")).Result;
            Assert.IsTrue(entityReturned != null);
        }

        [DataTestMethod]
        [DataRow("id1", "Contract 12/34 - Updated")]
        public void UpdateLegalContract(string Id, string Name)
        {
            Sam.DataModel.LegalContract entity = new Sam.DataModel.LegalContract { Id = Id, Name = Name };
            Sam.DataModel.LegalContract entityCreated = (Sam.DataModel.LegalContract)StaticFunctions.FabricAccess.UpdateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("Contract 12/34")]
        [DataRow("Contract 56/78")]
        public void DeleteLegalContract(string Name)
        {
            LegalContract entity = new LegalContract { Name = Name };
            LegalContract entityCreated = (LegalContract) StaticFunctions.FabricAccess.DeleteEntity(entity).Result;
        }

        [DataTestMethod]
        [DataRow("id1")]
        [DataRow("id2")]
        public void DeleteLegalContractById(string Id)
        {
            LegalContract entity = new LegalContract { Id = Id};
            LegalContract entityCreated = (LegalContract)StaticFunctions.FabricAccess.DeleteEntity(entity).Result;
        }

    }
}
