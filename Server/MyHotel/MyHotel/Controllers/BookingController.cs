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
                CVV= model.CVV,
                ExpiryDate= model.ExpiryDate



            };
            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();
            return Ok();



        }


        // [HttpGet("Invoice")]
        // public async Task<IActionResult> Invoice()
        // {
        //var bill = await _db.Payments.Include(i => i.Book).Where(i => i.Id == _db.Payments.BookId).FindAsyncAll();
        //return Ok(bill);



        // }


        [HttpGet("Invoice")]
        public async Task<IActionResult> Invoice(int Id)
        {
            Console.WriteLine(Id);
            var bill = _db.Payments
                .Include(m => m.Book)
                .Where(m => m.Id == Id).ToList();

            return Ok(bill);
        }




    }
}

