using Mango.Web.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MangoTests
{
    public class MangoRestaurantJsonTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SerializedObject_ReturnJsonString()
        {

            object jsonsObj = new
            {
                productId = 1,
                name = "Samosa",
                price = 15
            };
            var actual = JsonConvert.SerializeObject(jsonsObj);

            Assert.That(actual, Is.Not.Null);

        }

        [Test]
        public void DeserializedObject_ReturnJsonObject()
        {

           
            var jsonString = "[{'productId':1,'name':'Samosa','price':15}]";

            var actual = JsonConvert.DeserializeObject(jsonString);

            Assert.That(actual, Is.Not.Null);

        }

        [Test]
        public void DeserialzedObject_InputJsonString_ReturnProductDto()
        {

            object jsonsObj = new
            {
                productId = 1,
                name = "Samosa",
                price = 15
            };
            //var jsonObject = new object(){ productId:1, name: "Samosa", price:15 };
            var jsonString = "[{'productId':1,'name':'Samosa','price':15}]";
            //var actual = JsonConvert.SerializeObject(jsonsObj);
            var actual2 = JsonConvert.DeserializeObject(jsonString);
           
            //Assert.That(actual, Is.Not.Null);
            Assert.That(actual2, Is.Not.Null);

        }

        [Test]
        public void DeserialzedObject_InputJsonString_ReturnProductDto2()
        {

            object jsonsObj = new
            {
                productId = 1,
                name = "Samosa",
                price = 15
            };
            //var jsonObject = new object(){ productId:1, name: "Samosa", price:15 };
            var jsonString = "[{'productId':1,'name':'Samosa','price':15}]";
            //var actual = JsonConvert.SerializeObject(jsonsObj);
            var actual2 = JsonConvert.DeserializeObject<List<object>>(jsonString);

            //Assert.That(actual, Is.Not.Null);
            Assert.That(actual2, Is.Not.Null);

        }




    }
}