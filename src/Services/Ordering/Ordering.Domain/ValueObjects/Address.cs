namespace Ordering.Domain.ValueObjects;

public class Address
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;
    public string AddressLine { get; private set; } = default!;
    public string? AddressLine2 { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;

    protected Address() { }

    private Address(
        string firstName,
        string lastName,
        string emailAddress,
        string addressLine1,
        string? addressLine2,
        string country,
        string state,
        string zipCode
    )
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine1;
        AddressLine2 = addressLine2;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(
        string firstName,
        string lastName,
        string emailAddress,
        string addressLine1,
        string? addressLine2,
        string country,
        string state,
        string zipCode
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(emailAddress);
        ArgumentException.ThrowIfNullOrEmpty(addressLine1);

        return new Address(
            firstName,
            lastName,
            emailAddress,
            addressLine1,
            addressLine2,
            country,
            state,
            zipCode
        );
    }
}
