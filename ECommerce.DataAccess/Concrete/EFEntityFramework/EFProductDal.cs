using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstraction;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Concrete.EFEntityFramework
{
    public class EFProductDal:EFEntityFrameworkRepositoryBase<Product,NorthwindContext>,IProductDal
    {
        //Demeli, burda olunan deyisikliklerin izahini burda yazacam. Ilk olaraq Scaffold-command-i ile existing Database 
        //with CodeFirst modeli ile isleyirik. Ve Scaffold-ozu default olaraq "NorthwindContext"-i hem DEFAULT hem de 
        //PARAMETRIC constructoru ile birge yaratdi dbcontext-i.Ona gore de her defe /Product-endpointine sorgu atilanda 
        //default constructoru ise dusurdu ve NorthwindContext-deki "OnConfiguring"-metoduna dusub orda inject edirdi database-i
        //ve elemek istediyimiz Program.cs-de yazdigimiz services-qeydiyyatdan kecirib inject etmek alinmirdi.
        //Ona gore de, NorthwindContext-in default-contructorunu sildik,cunki,burda EFProductDal-da idi problem,
        //NorthwindContext-i qebul edirdi, ve iceride using-bloklarinin icinde onun obyektini yaradirdiq ve icine parametr
        //gonderilmirdi deye default constructoru ise dusurdu ve "OnConfiguring"-metoduna gedirdi,bu metodu silende de error
        //verirdi. Ve men bunu istifade etmirem,onsuzda hemin bu context-i Program.cs-de qeydiyyatdan kecirdiyime gore
        //context-i harda cagirsam avtomatik onun icine inject olacaq,mende onu burda EFProductDal-da cagiriram,hemin bura
        //gelen avtomatik gedir base-classa ":base(context)"-yeni "EFEntityFrameworkRepositoryBase"-classina,ve ordaki 
        //this._context-e gonderdiyim context-yazilir ve artiq yene "generic"-lik pozulmadi,sadece bir defe colden 
        //gonderdik. Ve birde TContext-in where ile olan : new()-u sildim,cunki NorthwindContext-in default construktorunu
        //sildim ve ona gorede obyekti yaranabilen oldugunu basa dusmediyine gore error verirdi.
        public EFProductDal(NorthwindContext context)
            :base(context)
        {
        }
    }
}
