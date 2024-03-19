import { Component } from '@angular/core';
import { IUser } from '../interfaces/user';
import { PostService } from '../services/post.service';
import { IConnection } from '../interfaces/connection';

@Component({
  selector: 'app-connection-lists',
  templateUrl: './connection-lists.component.html',
  styleUrl: './connection-lists.component.scss',
})
export class ConnectionListsComponent {
  public userList!: IUser[];
  public connectionList: IConnection[] = [];

  constructor(private postService: PostService) {}
  ngOnInit(): void {
    this.getAllUsers();
    this.getAllConnections();
  }

  public getAllUsers() {
    this.postService.getAllUsers().subscribe({
      next: res => {
        console.log(res);
        this.userList = res.data.reverse();
      },
      error: error => {
        console.log('Some Error occurs: ', error);
      },
    });
  }
  public getAllConnections() {
    this.postService.getMyConnections().subscribe({
      next: res => {
        console.log(res);
        this.connectionList = res.data || [];
      },
      error: error => {
        console.log('Some Error occurs: ', error);
      },
    });
  }
  public connectUser(receiverId: string) {
    const connectionDto: IConnection = { receiverId: receiverId };
    this.postService.connectUsers(receiverId).subscribe({
      next: response => {
        console.log('Connection request sent successfully.');
        const connection = this.connectionList.find(c => c.receiverId === receiverId);
        if (connection) {
          connection.requestStatus = 'Pending';
        }
        this.getAllConnections();
      },
      error: error => {
        console.error('Error while sending connection request:', error);
      },
    });
  }

  public isConnectionPending(receiverId: string): boolean {
    const connection = this.connectionList.find(c => c.receiverId === receiverId);
    return connection && connection.requestStatus === 'Pending';
  }
  public isConnectionAccepted(receiverId: string): boolean {
    const connection = this.connectionList.find(c => c.receiverId === receiverId);
    return connection && connection.requestStatus === 'Accepted';
  }

  public isConnectionRejected(receiverId: string): boolean {
    const connection = this.connectionList.find(c => c.receiverId === receiverId);
    return connection && connection.requestStatus === 'Rejected';
  }
}
