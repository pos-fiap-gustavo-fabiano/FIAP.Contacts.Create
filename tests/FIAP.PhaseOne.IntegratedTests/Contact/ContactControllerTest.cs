using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FIAP.Contacts.Create.Api.Dto;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.IntegratedTests;
using FIAP.Contacts.Create.Tests.Application;
using FIAP.Contacts.Create.Tests.Domain.Mock;
using Google.Protobuf.WellKnownTypes;

namespace FIAP.Contacts.Create.Tests.Presentation
{
    public class ContactControllerTest : IntegrationTest, IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory app;

        public ContactControllerTest(WebApplicationFactory app)
        {
            this.app = app;
        }

        [Fact]
        public async Task POST_AddContact_ReturnOK()
        {
            //Arrange
            var contact = new ContactDto()
            {
                Name = $"{_faker.Name.FirstName()} {_faker.Name.LastName()}",
                Email = _faker.Internet.Email(),
                Address = NewAddress(),
                Phone = NewPhone()
            };
            var client = app.CreateClient();
            //Act
            var result = await client.PostAsJsonAsync("api/contacts", contact);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task GET_ReturnExistContact_ReturnOk()
        {
            //Arrange
            var newContact = ContactMock.Create();
            app.Context.Contacts.Add(newContact);
            app.Context.SaveChanges();

            using var client = app.CreateClient();

            //Act
            var result = await client.GetAsync($"api/contacts/{newContact.Id}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var content = await result.Content.ReadAsStringAsync();
            Assert.NotNull(content);
            Assert.Contains("name", content);
            Assert.Contains("email", content);
        }

        [Fact]
        public async Task PUT_UpdateContact_ReturnOk()
        {
            // Arrange
            var client = app.CreateClient();
            var newContact = ContactMock.Create();
            app.Context.Contacts.Add(newContact);
            app.Context.SaveChanges();

            var id = newContact.Id;
            var contact = new ContactDto()
            {
                Name = $"{_faker.Name.FirstName()} {_faker.Name.LastName()}",
                Email = _faker.Internet.Email(),
                Address = NewAddress(),
                Phone = NewPhone()
            };

            var content = new StringContent(
                JsonSerializer.Serialize(contact),
                Encoding.UTF8,
                "application/json");

            // Act
            var result = await client.PutAsync($"api/contacts/{id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode); 
        }

        [Fact]
        public async Task PUT_UpdateContact_WhenContactNotFound_ReturnsNotFound()
        {
            // Arrange
            var app = new WebApplicationFactory();
            var client = app.CreateClient();
            var nonExistentId = Guid.NewGuid(); // Gera um ID que não existe

            var contact = new ContactDto()
            {
                Name = $"{_faker.Name.FirstName()} {_faker.Name.LastName()}",
                Email = _faker.Internet.Email(),
                Address = NewAddress(),
                Phone = NewPhone()
            };

            var content = new StringContent(
                JsonSerializer.Serialize(contact),
                Encoding.UTF8,
                "application/json");

            // Act
            var result = await client.PutAsync($"api/contacts/{nonExistentId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode); //
        }
            
        [Fact]
        public async Task DELETE_DeleteContact_ReturnNoContent()
        {
            // Arrange
            var newContact = ContactMock.Create();
            app.Context.Contacts.Add(newContact);
            app.Context.SaveChanges();

            var client = app.CreateClient();
            // Act
            var result = await client.DeleteAsync($"api/contacts/{newContact.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task DELETE_DeleteContact_WhenContactNotFound_ReturnsNotFound()
        {
            // Arrange
            var app = new WebApplicationFactory();
            var client = app.CreateClient();
            var nonExistentId = Guid.NewGuid(); // Gera um ID que não existe

            // Act
            var result = await client.DeleteAsync($"api/contacts/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode); 
        }


        private PhoneDto NewPhone() =>
            new PhoneDto
            {
                DDD = new Random().Next(1, 99),
                Number = _faker.Phone.PhoneNumber("#########")
            };

        private AddressDto NewAddress() =>
            new AddressDto
            {
                Street = _faker.Address.StreetName(),
                Number = _faker.Address.BuildingNumber(),
                City = _faker.Address.City(),
                Complement = _faker.Address.SecondaryAddress(),
                State = _faker.Address.StateAbbr(),
                District = _faker.Address.County(),
                Zipcode = _faker.Address.ZipCode("########")
            };
    }
}
