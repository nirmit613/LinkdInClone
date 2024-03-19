export interface IPost {
  id: string;
  content?: string;
  imageUrl?: File | null;
  userId: string;
  creationTime: string;
  user?: {
    userName: string;
  };
  totalLikes?: number;
  liked?: boolean;
  showComments?: boolean;
  newComment?: string;
  comments?: Comment[];
}
