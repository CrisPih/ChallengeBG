using System.ComponentModel.DataAnnotations;

namespace ChallengeBG.Application.DTOs
{
    #region requests
    public record ProductoProveedorRequest(
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un proveedor")] int proveedorId,
        [Required(ErrorMessage = "El número de lote es obligatorio")] string numeroLote,
        [Range(0, 99999999, ErrorMessage = "El precio debe ser mayor o igual a 0")] decimal precio,
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")] int stock);

    public record CreateProductoRequest(
        [Required(ErrorMessage = "El nombre es obligatorio")] string nombre,
        string? descripcion,
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría")] int categoriaId,
        [MinLength(1, ErrorMessage = "Agregue al menos un precio")] List<ProductoProveedorRequest> precios);

    public record UpdateProductoRequest(int id,
        [Required(ErrorMessage = "El nombre es obligatorio")] string nombre,
        string? descripcion,
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una categoría")] int categoriaId,
        [MinLength(1, ErrorMessage = "Agregue al menos un precio")] List<ProductoProveedorRequest> precios);
    #endregion

    #region responses
    public record GetProductoProveedorResponse(int proveedorId, string proveedorNombre, string numeroLote,
        decimal precio, int stock);

    public record GetProductoResponse(int id, string nombre, string? descripcion,int categoriaId, string categoriaNombre,
    List<GetProductoProveedorResponse> precios);
    #endregion
}