using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace TodoApp
{
    //Deleting soft
    //FullAuditedAggregateRoot => Who created- when - is deleted - who modified and when properties
    public  class TodoItem:BasicAggregateRoot<Guid>,ISoftDelete,IMultiTenant
    {
        /*public TodoItem(Guid id):base(id)
}*/
        public string Text { get; set; }
        public bool IsDeleted { get; set; }

        /*Multi Tenant: A single Software and Database that one customer at the same time.This users cannot see each other's data  */
        public Guid? TenantId { get; set; } 

    }
}
