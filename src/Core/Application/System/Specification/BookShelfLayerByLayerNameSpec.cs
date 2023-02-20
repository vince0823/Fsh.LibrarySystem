using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Specification;
public class BookShelfLayerByLayerNameSpec : Specification<BookShelfLayer>, ISingleResultSpecification
{
    public BookShelfLayerByLayerNameSpec(string layerName, Guid bookShelfId) =>
      Query.Where(b => b.LayerName == layerName && b.BookShelfId == bookShelfId);
    public BookShelfLayerByLayerNameSpec(string layerName, Guid bookShelfId, Guid oldId) =>
     Query.Where(b => b.LayerName == layerName && b.BookShelfId == bookShelfId && b.Id != oldId);

}