using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonApp.Models
{
    public class OrderMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }  // Link this to customer table
        [ForeignKey("CustomerId")]           // Potential problem // Caps ID // Works so far
        public TblCustomerReg TblCustomerReg { get; set; }

        [Display(Name = "Date Ordered")]
        public DateTime DateOrdered { get; set; }

        [Display(Name = "Time Ordered")]
        public TimeSpan TimeOrdered { get; set; }

        public double FinalPrice { get; set; }

        public string Status { get; set; }  // Cart-Pending / Approved / Cancelled
                                            // Please remember to display in Admin Order History


        /// <summary>
        /// New Add on for deliveries
        /// </summary>
        /// 


        public int? DriverId { get; set; }

        [ForeignKey("DriverId")]
        public virtual TblDriverForLogin TblDriverForLogin { get; set; }

        public DateTime? deliveryDate { get; set; } // this is estimated delivery date







        public string DeliveryAddress { get; set; }

        public string DeliverySuburb { get; set; }



        public string DeliveryZipCode { get; set; }   ///after delivery view


        public DeliveryType Delivery { get; set; }

        public enum DeliveryType
        {
            [Display(Name = "Standard Delivery")]

            Standard_Delivery,

            [Display(Name = "Express Delivery")]
            Express_Delivery,

            Pickup

        }

        public double GetDeliveryPrice(DeliveryType objDel)
        {
            double Cost = 0.0;
            if (objDel == DeliveryType.Standard_Delivery)
                Cost = 60;
            else if (objDel == DeliveryType.Express_Delivery)
                Cost = 140;

            return Cost;
        }


        public double CalcFinalTotal()
        {
            double TotalWithShipping = FinalPrice;

            if (Delivery == DeliveryType.Standard_Delivery)
            {
                TotalWithShipping += 60;

            }
            else
               if (Delivery == DeliveryType.Express_Delivery)
            {
                TotalWithShipping += 140;

            }
            else
                if (Delivery == DeliveryType.Pickup)
            {
                TotalWithShipping += 0;



            }

            return TotalWithShipping;

        }



        public DeliveryStatus deliverystatus { get; set; }

        public enum DeliveryStatus
        {
            Processing,  //waiting for admin to accept

            [Display(Name = "En-Route")]
            En_Route,  //when driver logs in and accepts delivery


            Delivered,  //when driver confirms delivery

            [Display(Name = "Requesting Refund")]
            Requesting_Refund,
                   
            [Display(Name ="Refund Accepted")]
            Refund_Accepted,
           
            [Display(Name ="Refund Denied")]
            Refund_Denied,


           Accepted,  //admin has accepted
     

        }



        public string DeliveryID { get; set; }

        public OrderMain()
        {
            DeliveryID = Guid.NewGuid().ToString();

        }



        public DateTime CalcDeliveryDate()
        {

            DateTime deliveryDate = DateTime.Now;



            if (Delivery == DeliveryType.Standard_Delivery)
            {
                deliveryDate = deliveryDate.AddDays(4);

            }
            else
              if (Delivery == DeliveryType.Express_Delivery)
            {
                deliveryDate = deliveryDate.AddDays(2);

            }
            else
               if (Delivery == DeliveryType.Pickup)
            {
                deliveryDate = deliveryDate.AddDays(4);



            }

            return deliveryDate;




        }
    }
}