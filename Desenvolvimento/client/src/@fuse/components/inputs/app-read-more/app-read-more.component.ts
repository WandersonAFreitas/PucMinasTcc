import { Component, Input, OnChanges, OnInit, OnDestroy, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-read-more',
  templateUrl: './app-read-more.component.html',
  styleUrls: ['./app-read-more.component.scss'],
})
export class ReadMoreComponent implements OnInit, OnDestroy, AfterViewInit, OnChanges {

  @Input() text: string;

  @Input() minWords = 15;

  @Input() moreText = 'ler mais';
  @Input() lessText = 'ler menos';

  public fullText = true;
  public showMore = false;
  public showLess = false;
  public rmTextShort = '';
  public rmTextFull = '';
  public inputWords = [];

  constructor() {
  }

  readMore(flag: boolean) {
    if (flag) {
      this.showMore = false;
      this.fullText = true;
      this.rmTextFull = this.text;
      this.showLess = true;
    } else {
      this.showLess = false;
      this.showMore = true;
      this.fullText = false;
    }
  }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  ngAfterViewInit() {

  }
  ngOnChanges() {
    this.rmTextShort = this.text;
    this.rmTextFull = this.text;
    this.inputWords = this.text.split(' ');
    if (this.inputWords.length > this.minWords) {
      this.fullText = false;
      this.showMore = true;
      this.rmTextShort = this.inputWords.slice(0, this.minWords).join(' ') + '...';
    } else {
      if (this.rmTextShort.length > 300) {
        this.fullText = false;
        this.showMore = true;
        this.rmTextShort = this.rmTextShort.substr(0, 300) + '...'
      } else {
        const lineBreaks = this.rmTextShort.split(/\n/g)
        if (lineBreaks.length > 4) {
          this.fullText = false
          this.showMore = true
          this.rmTextShort = lineBreaks.slice(0, 4).join('\n') + '...'
        }
      }
    }
  }
}
