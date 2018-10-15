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


        FabricJsonAccess _fabricAccess = new FabricJsonAccess(
            @"https://localhost:44329/api/",
            "33c35730-2deb-44ae-9a16-1dec27960052");

        //[DataTestMethod]
        //[DataRow("id1", "ManchesterUnited", "CK-1234-MANU", 0)]
        //public async Task<Party> CreateParty(string Id, string Name, string IdentificationNumber, PartyType Type)
        //{
        //    Party entity = new Party { Id = Id, Name = Name, IdentificationNumber = IdentificationNumber, Type = Type };
        //    Party entityCreated = (Party)await _fabricAccess.CreateEntity(entity);
        //    return entityCreated;
        //}



        [DataTestMethod]
        [DataRow("Contract 12/34")]
        public void CreateLegalContract(string Name)
        {
            string id = Guid.NewGuid().ToString();
            Sam.DataModel.LegalContract entity = new Sam.DataModel.LegalContract { Id = id, Name = Name};
            Sam.DataModel.LegalContract entityCreated = (Sam.DataModel.LegalContract)_fabricAccess.CreateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }
        [DataTestMethod]
        [DataRow("Contract 12/34")]
        public void GetLegalContract(string Name)
        {
            LegalContract entity = new LegalContract { Name = Name };
            LegalContract entityReturned = (LegalContract) _fabricAccess.GetEntity(entity, _fabricAccess.BuildFilterList("Name")).Result;
            Assert.IsTrue(entityReturned != null);
        }


        //public async Task<BarUser> DeleteUser(string UserName)
        //{
        //    BarUser entity = new BarUser { UserName = UserName };
        //    BarUser entityCreated = (BarUser)await _fabricAccess.DeleteEntity(entity);
        //    return entityCreated;
        //}

        //public async Task<BarUser> GetUser(string Id)
        //{
        //    BarUser entity = new BarUser { Id = Id };
        //    BarUser entityReturned = (BarUser)await _fabricAccess.GetEntity(entity, _fabricAccess.BuildFilterList("Id"));
        //    return entityReturned;
        //}





    }
}
