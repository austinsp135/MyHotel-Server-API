import { Component } from '@angular/core';
import JwtHelper from 'src/app/helpers/jwtHelper';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'app-viewbookings',
  templateUrl: './viewbookings.component.html',
  styleUrls: ['./viewbookings.component.css']
})
export class ViewbookingsComponent {
    bookings:any

	constructor (private adminService: RoomService, private jwt: JwtHelper) {}


	ngOnInit() {
		this.adminService.viewBookings().subscribe({
			next: (data:any) => {
                console.log(data);
				this.bookings = data;
			}
		});
	}
}
