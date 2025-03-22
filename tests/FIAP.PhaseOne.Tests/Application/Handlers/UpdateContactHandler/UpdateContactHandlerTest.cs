using ErrorOr;
using FIAP.Contacts.Create.Application.Dto;
using FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact;
using FIAP.Contacts.Create.Application.Shared;
using FIAP.Contacts.Create.Domain.ContactAggregate;
using FIAP.Contacts.Create.Tests.Domain.Mock;

namespace FIAP.Contacts.Create.Tests.Application.Handlers.UpdateContactHandler
{
    public class UpdateContactHandlerTest : ApplicationTest
    {

        [Fact]
        public async Task UpdateContact_WithValidData_Succeeded()
        {
            var id = Guid.NewGuid();
            var contact = ContactMock.Create();

            _contactRepositoryMock
                .Setup(x =>
                    x.GetById(id, _ct))
                .ReturnsAsync(
                    contact);

            var request = new UpdateContactRequest
            {
                Id = id,
                Contact = new FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Dto.ContactForUpdateDto(
                    _faker.Name.FullName(),
                    NewPhone(),
                    _faker.Internet.Email(),
                    NewAddress())
            };

            var response = await _mediator.Send(request, _ct);

            Assert.False(response.IsError);

            _contactRepositoryMock.Verify(
                x => x.SaveChanges(_ct),
                Times.Once
                );

            Assert.NotNull(response.Value);
        }

        [Fact]
        public async Task UpdateContact_NotFoundContact_Failed()
        {
            var id = Guid.NewGuid();
            var contact = ContactMock.Create();

            _contactRepositoryMock
                .Setup(x =>
                    x.GetById(id, _ct))
                .Returns(Task.FromResult<Contact?>(null));

            var request = new UpdateContactRequest
            {
                Id = id,
                Contact = new FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Dto.ContactForUpdateDto(
                    _faker.Name.FullName(),
                    NewPhone(),
                    _faker.Internet.Email(),
                    NewAddress())
            };

            var response = await _mediator.Send(request, _ct);

            AssertFailed(response);

            Assert.Contains(response.Errors, x => x.Description.Contains("Not Found"));

        }


        [Fact]
        public async Task UpdateContact_WithEmptyId_Failed()
        {
            var request = new UpdateContactRequest
            {
                Id = Guid.Empty,
                Contact = new FIAP.Contacts.Create.Application.Handlers.Commands.UpdateContact.Dto.ContactForUpdateDto(
                    _faker.Name.FullName(),
                    NewPhone(),
                    _faker.Internet.Email(),
                    NewAddress())
            };

            var response = await _mediator.Send(request, _ct);

            Assert.Contains(response.Errors, x => x.Description == DefaultFailures.NotEmpty);

            AssertFailed(response);
        }

        
        private void AssertFailed(ErrorOr<Updated> response)
        {
            Assert.True(response.IsError);

            _contactRepositoryMock.Verify(
                x => x.SaveChanges(_ct),
                Times.Never
                );
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

