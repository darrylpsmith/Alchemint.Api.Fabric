using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alchemint.Client.JsonAccess;
using Sam.DataModel;
using System.Threading.Tasks;
using System;

namespace Sam.Api.UnitTests
{
    [TestClass]
    public class LegalContractSectionTests
    {

        [DataTestMethod]
        [DataRow("RepresentClub", "I agree to represent the club in 2019", 0)]
        [DataRow("GoalBonus", "Goal bonus clauses here", 0)]
        public void CreateLegalContractSection(string Name, string Content, int PositionInContract)
        {
            Sam.DataModel.LegalContractSection entity = new Sam.DataModel.LegalContractSection {Name = Name, Content = Content, PositionInContract = PositionInContract };
            Sam.DataModel.LegalContractSection entityCreated = (Sam.DataModel.LegalContractSection)StaticFunctions.FabricAccess.CreateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("id1", "EndorseKit", "I agree use kit", 0)]
        [DataRow("id2", "Leave", "20 Days Leave per year", 0)]
        public void CreateLegalContractSectionWithId(string Id, string Name, string Content, int PositionInContract)
        {
            Sam.DataModel.LegalContractSection entity = new Sam.DataModel.LegalContractSection { Id = Id, Name = Name, Content = Content, PositionInContract = PositionInContract };
            Sam.DataModel.LegalContractSection entityCreated = (Sam.DataModel.LegalContractSection)StaticFunctions.FabricAccess.CreateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("RepresentClub")]
        [DataRow("GoalBonus")]
        public void GetLegalContractSectionByName(string Name)
        {
            LegalContractSection entity = new LegalContractSection { Name = Name };
            LegalContractSection entityReturned = (LegalContractSection)StaticFunctions.FabricAccess.GetEntity(entity, StaticFunctions.FabricAccess.BuildFilterList("Name")).Result;
            Assert.IsTrue(entityReturned != null);
        }

        [DataTestMethod]
        [DataRow("id1")]
        [DataRow("id2")]
        public void GetLegalContractSectionById(string Id)
        {
            LegalContractSection entity = new LegalContractSection { Id = Id };
            LegalContractSection entityReturned = (LegalContractSection)StaticFunctions.FabricAccess.GetEntity(entity, StaticFunctions.FabricAccess.BuildFilterList("Id")).Result;
            Assert.IsTrue(entityReturned != null);
        }


        [DataTestMethod]
        [DataRow("RepresentClub", "I agree to represent the club in 2019", 0)]
        [DataRow("GoalBonus", "Goal bonus clauses here", 0)]
        public void UpdateLegalContractSection(string Name, string Content, int PositionInContract)
        {
            Sam.DataModel.LegalContractSection entity = new Sam.DataModel.LegalContractSection {Name = Name, Content = Content, PositionInContract = PositionInContract };
            Sam.DataModel.LegalContractSection entityCreated = (Sam.DataModel.LegalContractSection)StaticFunctions.FabricAccess.UpdateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("id1", "RepresentClub", "MOD: I agree to represent the club in 2019", 0)]
        [DataRow("id2", "GoalBonus", "MOD: Goal bonus clauses here", 0)]
        public void UpdateLegalContractSectionById(string Id, string Name, string Content, int PositionInContract)
        {
            Sam.DataModel.LegalContractSection entity = new Sam.DataModel.LegalContractSection { Id = Id, Name = Name, Content = Content, PositionInContract = PositionInContract };
            Sam.DataModel.LegalContractSection entityCreated = (Sam.DataModel.LegalContractSection)StaticFunctions.FabricAccess.UpdateEntity(entity).Result;
            Assert.IsTrue(entityCreated != null);
        }

        [DataTestMethod]
        [DataRow("RepresentClub")]
        [DataRow("GoalBonus")]
        public void DeleteLegalContractSection(string Name)
        {
            LegalContractSection entity = new LegalContractSection { Name = Name };
            LegalContractSection entityCreated = (LegalContractSection)StaticFunctions.FabricAccess.DeleteEntity(entity).Result;
        }

        [DataTestMethod]
        [DataRow("id1")]
        [DataRow("id2")]
        public void DeleteLegalContractSectionById(string Id)
        {
            LegalContractSection entity = new LegalContractSection { Id = Id };
            LegalContractSection entityCreated = (LegalContractSection)StaticFunctions.FabricAccess.DeleteEntity(entity).Result;
        }

        //[DataTestMethod]
        //[DataRow("id1", "ManchesterUnited", "CK-1234-MANU", 0)]
        //public async Task<Party> CreateParty(string Id, string Name, string IdentificationNumber, PartyType Type)
        //{
        //    Party entity = new Party { Id = Id, Name = Name, IdentificationNumber = IdentificationNumber, Type = Type };
        //    Party entityCreated = (Party)await _fabricAccess.CreateEntity(entity);
        //    return entityCreated;
        //}



        //[DataTestMethod]
        //[DataRow("Contract 12/34")]
        //public void GetLegalContractSection(string Name)
        //{
        //    LegalContract entity = new LegalContract { Name = Name };
        //    LegalContract entityReturned = (LegalContract) _fabricAccess.GetEntity(entity, _fabricAccess.BuildFilterList("Name")).Result;
        //    Assert.IsTrue(entityReturned != null);
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
