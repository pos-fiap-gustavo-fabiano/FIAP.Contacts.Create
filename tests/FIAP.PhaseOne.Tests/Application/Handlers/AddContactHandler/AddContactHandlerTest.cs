using ErrorOr;
using FIAP.Contacts.Create.Application.Dto;
using FIAP.Contacts.Create.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Create.Application.Shared;
using FIAP.Contacts.Create.Domain.ContactAggregate;
using Moq;

namespace FIAP.Contacts.Create.Tests.Application.Handlers.AddContactHandler
{
    public class AddContactHandlerTest : ApplicationTest
    {
        [Fact]
        public async Task CreateContact_WithValidData_Succeeded()
        {
            var request = new AddContactRequest
            {
                Name = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Phone =  NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.False(response.IsError);

            _contactRepositoryMock.Verify(
                x => x.Add(It.IsAny<Contact>(), _ct),
                Times.Once
                );

            Assert.True(response.Value.Id != Guid.Empty);

        }

        [Theory]
        [InlineData("ASDASDASDa")]
        [InlineData("415165156")]
        [InlineData("I&*&*&*&**")]
        public async Task CreateContact_WithInvalidName_Failed(string name)
        {
            var request = new AddContactRequest
            {
                Name = name,
                Email = _faker.Internet.Email(),
                Phone = NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.InvalidName);

            AssertFailed(response);
        }

        [Fact]
        public async Task CreateContact_WithLargeName_Failed()
        {
            var name = "Nome muito grande Nome muito grande Nome muito grande Nome muito grande grande Nome muito grande Nome muito grande grande Nome muito grande Nome muito grande grande Nome muito grande Nome muito grande";
            var request = new AddContactRequest
            {
                Name = name,
                Email = _faker.Internet.Email(),
                Phone = NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.MaxLength);

            AssertFailed(response);
        }

        

        [Fact]
        public async Task CreateContact_WithEmptyName_Failed()
        {
            var name = string.Empty;
            var request = new AddContactRequest
            {
                Name = name,
                Email = _faker.Internet.Email(),
                Phone =  NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.NotEmpty);

            AssertFailed(response);
        }

        [Fact]
        public async Task CreateContact_WithEmptyEmail_Failed()
        {
            var request = new AddContactRequest
            {
                Name = _faker.Name.FullName(),
                Email = string.Empty,
                Phone = NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.NotEmpty);

            AssertFailed(response);
        }

        [Fact]
        public async Task CreateContact_WithInvalidEmail_Failed()
        {
            var request = new AddContactRequest
            {
                Name = _faker.Name.FullName(),
                Email = "email.com.br",
                Phone = NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.InvalidEmail);

            AssertFailed(response);
        }

        [Fact]
        public async Task CreateContact_WithLargeEmail_Failed()
        {
            var request = new AddContactRequest
            {
                Name = _faker.Name.FullName(),
                Email = "emaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaail@com.br",
                Phone = NewPhone(),
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.MaxLength);

            AssertFailed(response);
        }

        [Fact]
        public async Task CreateContact_WithEmptyPhone_Failed()
        {
            var request = new AddContactRequest
            {
                Name = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Phone = null!,
                Address = NewAddress()
            };

            var response = await _mediator.Send(request, _ct);

            AssertFailed(response);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.NotEmpty);

        }

        [Fact]
        public async Task CreateContact_WithEmptyAddress_Failed()
        {
            var request = new AddContactRequest
            {
                Name = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Phone = NewPhone(),
                Address = null!
            };

            var response = await _mediator.Send(request, _ct);

            AssertFailed(response);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.NotEmpty);

        }

        private void AssertFailed(ErrorOr<AddContactResponse> response)
        {
            Assert.True(response.IsError);
            _contactRepositoryMock.Verify(
                x => x.Add(It.IsAny<Contact>(), _ct),
                Times.Never
                );

            Assert.Null(response.Value);
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

