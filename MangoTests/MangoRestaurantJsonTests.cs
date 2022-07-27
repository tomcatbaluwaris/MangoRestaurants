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

            
//   @" {+
//  'isSucess': false",
//  "result": [
//    {
//                "productId": 1,
//      "name": "Samosa",
//      "price": 15,
//      "description": "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
//      "imageUrl": "https://dotnetmasterynew.blob.core.windows.net/mango/14.jpg",
//      "categoryName": "Appetizer"
//    },
//    {
//                "productId": 2,
//      "name": "Paneer Tikka",
//      "price": 13.99,
//      "description": "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
//      "imageUrl": "https://dotnetmasterynew.blob.core.windows.net/mango/12.jpg",
//      "categoryName": "Appetizer"
//    },
   
//  ],
//  "displayMessage": null,
//  "errorMessages": null
//}";

        object jsonsObj = new
            {
                productId = 1,
                name = "Samosa",
                price = 15
            };
            //var jsonObject = new object(){ productId:1, name: "Samosa", price:15 };
            var jsonString = "[{'productId':1,'name':'Samosa','price':15}]";
            //var actual = JsonConvert.SerializeObject(jsonsObj);
            var actual2 = JsonConvert.DeserializeObject<List<ProductDto>>(jsonString);

            //Assert.That(actual, Is.Not.Null);
            Assert.That(actual2, Is.Not.Null);

        }




    }
}