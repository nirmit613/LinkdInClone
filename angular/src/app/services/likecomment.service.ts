import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LikecommentService {
  constructor(private http: HttpClient) {}

  public getTotalLikesByPostId(postId: string): Observable<any> {
    return this.http.get<any>(`${environment.apis.default.url}/api/likes/${postId}/count`);
  }
  public addLikePost(postId: string): Observable<any> {
    const likeData = { postId: postId };
    return this.http.post<any>(`${environment.apis.default.url}/api/likes/like`, likeData);
  }

  public unLikePost(postId: string): Observable<any> {
    const unLikeData = { postId: postId };
    return this.http.post<any>(`${environment.apis.default.url}/api/likes/unlike`, unLikeData);
  }
  public isPostLikeByUser(postId: string): Observable<any> {
    return this.http.get<any>(
      `${environment.apis.default.url}/api/likes/isPostLikedByUser?postId=${postId}`
    );
  }

  public addComment(comment: any): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<any>(`${environment.apis.default.url}/api/comments/addComment`, comment, {
      headers,
    });
  }
  public getCommentsOfPost(postId: string): Observable<any> {
    return this.http.get<any>(
      `${environment.apis.default.url}/api/comments/postId?postId=${postId}`
    );
  }
}
