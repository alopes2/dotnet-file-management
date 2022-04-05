import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { Image } from './Image';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'FileManagement';
  images: Image[] = [];
  newImage: any;

  private baseAddress = 'https://localhost:7227';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http
      .get<Image[]>(this.baseAddress + '/images')
      .subscribe((response) => (this.images = response));
  }

  onFileSelected(event: any) {
    if (event?.target?.files != null) {
      this.newImage = event.target.files[0];
    }
  }

  onUploadImage() {
    const formData = new FormData();
    formData.append('image', this.newImage);

    this.http.post<Image>(this.baseAddress + '/images', formData).subscribe(
      (response: Image) => {
        console.log(response);
        this.images = [
          ...this.images,
          response
        ];
      },
      (error) => console.error(error)
    );
  }
}
