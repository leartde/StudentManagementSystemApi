namespace API.Shared.DTOs.ContactInfoDtos;

public class AddContactInfoDto
{
  public string Country { get; set; } = string.Empty;
  public string Residence { get; set; } = string.Empty;
  public string Street { get; set; } = string.Empty;
  public string ZipCode { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string PrivateEmail { get; set; } = string.Empty;
  public string Ethnicity { get; set; } = string.Empty;
  public string Nationality { get; set; } = string.Empty;
}
