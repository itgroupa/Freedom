using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.DataSchema.Interfaces;

public interface ICertificateSchemaService
{
    Task<CertificateData> GetAsync(Guid id);
    Task<byte[]> GetDataAsync(Guid id);
    Task<ResponsePaginator<CertificateData>> GetAsync(RequestPaginator request);
    Task<CertificateData> AddAsync(AddCertificateData model);
    Task<CertificateData> UpdateAsync(UpdateCertificateData model);
    Task DeleteAsync(Guid id);
}
