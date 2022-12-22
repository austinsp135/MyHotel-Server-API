using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHotel.Data;
using MyHotel.Models;
using MyHotel.Models.RequestModels;
using System.Drawing;
using System;
using System.Security.Claims;

namespace MyHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;



        public BookingController(ApplicationDbContext db)

        {

            _db = db;

        }

        [HttpGet]

        public async Task<ActionResult> GetHotels()

        {
            var rooms = await _db.Rooms.ToListAsync();
            return Ok(new ResponseModel<IEnumerable<Room>>()
            {
                Data = rooms
            });
        }

        [HttpGet("ViewBooking")]

        public async Task<IActionResult> ViewBooking()
        {
            var cid = HttpContext.User.FindFirstValue("UserId");
            var booking = _db.Bookings.Where(i => i.CustomerId == cid).FirstOrDefault();
            //var booking = await _db.Bookings.ToListAsync();
            return Ok(new ResponseModel<Booking>()
            {
                Data = booking
            });
        }

        [HttpGet("FindRoom")]

        public async Task<ActionResult> FindRoom(int id)

        {
            var roomId = _db.Rooms.Where(i => i.Id == id).FirstOrDefault();
            //var rooms = await _db.Rooms.ToListAsync();
            return Ok(new ResponseModel<Room>()
            {
                Data = roomId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Booking(BookingRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var booking = new Booking()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                Guest = model.Guest,
                NoRoom = model.NoRoom,
                CustomerId = model.CustomerId,
                RoomId = model.RoomId

            };
            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            return Ok(new ResponseModel<Booking>()
            {
                Data = booking
            });
        }

        //public class book
        //{
        //    public string customerId { get; set; }
        //    public int bookingId { get; set; }
        //    public int amount { get; set; }
        //    public string cardHolder { get; set; }
        //    public string cardNumber { get; set; }
        //    public CardType cardType { get; set; }
        //}

        [HttpPost("Payment")]
        public async Task<IActionResult> Payment(PaymentRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var payment = new Payment()
            {



                CustomerId = model.CustomerId,
                BookId= model.BookId,
                Amount = model.Amount,
                CardHolder = model.CardHolder,
                CardNumber = model.CardNumber,
                CardType = model.CardType,
                CVV = "1234",
                ExpiryDate = "12/2/2034"



            };
            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();
            return Ok();



        }


        //[HttpGet]
        //public IActionResult GetInvoice()
        //{

        //    [HttpGet]
        //    public IActionResult Get()
        //    var result = from person in _db.Payment join detail in _db.Bookings on  equals detail.Person
        //                 select new
        //                 {
        //                     id = person.Id,
        //                     firstname - person.Firstname,
        //                     lastname = person.Lastname,
        //                     detailText = detail.DetailText
        //                 };
        //                    return Ok(result);
        //}           


        //var result = from paymnent in _db.Payments
        //             join detail in _db.ApplicationUsers on  equals detail.PersonId
        //             into Details
        //             from defaultVal in Details.DefaultIfEmpty()
        //             select new
        //             {
        //                 id = person.Id,
        //                 firstname = person.Firstname,
        //                 lastname = person.Lastname,
        //                 detailText = defaultVal.DetailText
        //             };
        //return Ok(result);
    //}
}
}

