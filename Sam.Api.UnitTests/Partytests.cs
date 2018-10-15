using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alchemint.Client.JsonAccess;
using Sam.DataModel;
using System.Threading.Tasks;
using System;

namespace Sam.Api.UnitTests
{
    [TestClass]
    public class PartyTests
    {


        FabricJsonAccess _fabricAccess = new FabricJsonAccess(
            StaticFunctions.GetApiUrl(),
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
        [DataRow("LiverPool", "CK-1234-MLIV")]
        public void CreateParty(string Name, string IdentificationNumber)
        {
            Party entity = new Party { Id = Guid.NewGuid().ToString(), Name = Name, IdentificationNumber = IdentificationNumber};
            Party entityCreated = (Party)_fabricAccess.CreateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        //public async Task<BarUser> GetUser(string UserName, string Password)
        //{
        //    BarUser entity = new BarUser { UserName = UserName, Password = Password };
        //    BarUser entityReturned = (BarUser)await _fabricAccess.GetEntity(entity, _fabricAccess.BuildFilterList("UserName,Password"));
        //    return entityReturned;
        //}


        //[DataTestMethod]
        //[DataRow("Contract 56/78")]
        //public async Task<LegalContract> UpdateParty(string Name, string NewName)
        //{
        //    LegalContract entity = new LegalContract { Id = Id, UserName = UserName, Password = Password, Email = Email, Telephone = Telephone };
        //    LegalContract entityCreated = (LegalContract)await _fabricAccess.UpdateEntity(entity);
        //    return entityCreated;
        //}

        //public async Task<BarUser> GetUser(string UserName, string Password)
        //{
        //    BarUser entity = new BarUser { UserName = UserName, Password = Password };
        //    BarUser entityReturned = (BarUser)await _fabricAccess.GetEntity(entity, _fabricAccess.BuildFilterList("UserName,Password"));
        //    return entityReturned;
        //}



        //public async Task<BarUser> UpdateUser(string Id, string UserName, string Password, string Email, string Telephone)
        //{
        //    BarUser entity = new BarUser { Id = Id, UserName = UserName, Password = Password, Email = Email, Telephone = Telephone };
        //    BarUser entityCreated = (BarUser)await _fabricAccess.UpdateEntity(entity);
        //    return entityCreated;
        //}

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
