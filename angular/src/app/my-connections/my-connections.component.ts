import { Component, OnInit } from '@angular/core';
import { IUser } from '../interfaces/user';
import { PostService } from '../services/post.service';
import { IPost } from '../interfaces/post';
import { IConnection } from '../interfaces/connection';

@Component({
  selector: 'app-my-connections',
  templateUrl: './my-connections.component.html',
  styleUrl: './my-connections.component.scss',
})
export class MyConnectionsComponent {
  public myRequestList!: IPost[];
  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.getmyRequests();
  }

  public getmyRequests() {
    this.postService.getMyRequests().subscribe({
      next: res => {
        console.log(res);
        this.myRequestList = res.data;
      },
      error: error => {
        console.log('Some Error occurs: ', error);
      },
    });
  }
  public acceptRequest(request: IConnection): void {
    const updatedConnectionData = {
      id: request.id,
      requestStatus: 'Accepted',
      senderId: request.senderId,
    };
    this.postService.updateConnectionRequest(updatedConnectionData).subscribe({
      next: res => {
        console.log('Connection request accepted successfully:', res);
        this.getmyRequests();
      },
      error: error => {
        console.log('Error accepting connection request:', error);
      },
    });
  }
  public rejectRequest(request: IConnection): void {
    const updatedConnectionData = {
      id: request.id,
      requestStatus: 'Rejected',
      senderId: request.senderId,
    };
    this.postService.updateConnectionRequest(updatedConnectionData).subscribe({
      next: res => {
        console.log('Connection request Rejected:', res);
        this.getmyRequests();
      },
      error: error => {
        console.log('Error accepting connection request:', error);
      },
    });
  }
}
