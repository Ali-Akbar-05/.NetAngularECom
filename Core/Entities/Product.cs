using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Product : BaseEntity
{

    public string Name { get; set; } = default!;
    public string Description {get;set;} = default!;
    public decimal Price {get;set;}
    public string ProductUrl {get;set;} = default!;
    public ProductType ProductType {get;set;}
    public int ProductTypeId {get;set;}
    public ProductBrand ProductBrand {get;set;}
    public int ProductBrandId {get;set;}

}
