using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.Catalog;
public abstract class ProductChangeEvent : DomainEvent
{
    public Guid ProductId { get; set; }
    protected ProductChangeEvent(Guid productId) => ProductId = productId;
}

public class ProductDeleteEvent : ProductChangeEvent
{
    public ProductDeleteEvent(Guid productId)
        : base(productId)
    {

    }
}



