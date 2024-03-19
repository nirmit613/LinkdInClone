import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient) {}

  public getAllPosts(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/posts`);
  }
  public getAllUsers(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/posts/Users`);
  }
  public getPostsByUserId(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/posts/UserId`);
  }
  public getPostOfConnectedUsers(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/posts/ConnectionUsers`);
  }
  public addPost(post: any): Observable<any> {
    return this.http.post<any>(`${environment.apis.default.url}/api/posts/post`, post);
  }
  public connectUsers(receiverId: string): Observable<any> {
    const url = `${environment.apis.default.url}/api/connectionRequest/post`;
    const queryParams = { receiverId: receiverId };
    return this.http.post<any>(url, null, { params: queryParams });
  }
  public getMyRequests(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/connectionRequest/receiverId`);
  }
  public updateConnectionRequest(connData: any): Observable<any> {
    return this.http.put<any>(`${environment.apis.default.url}/api/connectionRequest`, connData);
  }
  public getAllConnections(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/connectionRequest`);
  }
  public getMyConnections(): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/connectionRequest/senderId`);
  }
}
