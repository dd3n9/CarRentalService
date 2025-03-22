import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: false,
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css',
})
export class PaginationComponent implements OnInit, OnChanges {
  @Input() currentPage: number = 1;
  @Input() totalPages: number = 1;
  @Output() pageChange = new EventEmitter<number>();

  pageNumbers: (number | string)[] = [];

  constructor() {}

  ngOnInit(): void {
    this.updatePageNumbersArray();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['totalPages'] || changes['currentPage']) {
      this.updatePageNumbersArray();
    }
  }

  onPageClick(pageNumber: number): void {
    if (
      pageNumber >= 1 &&
      pageNumber <= this.totalPages &&
      pageNumber !== this.currentPage
    ) {
      this.pageChange.emit(pageNumber);
    }
  }

  onPreviousPage(): void {
    if (this.currentPage > 1) {
      this.onPageClick(this.currentPage - 1);
    }
  }

  onNextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.onPageClick(this.currentPage + 1);
    }
  }

  private updatePageNumbersArray(): void {
    this.pageNumbers = [];
    if (this.totalPages <= 7) {
      for (let i = 1; i <= this.totalPages; i++) {
        this.pageNumbers.push(i);
      }
    } else {
      if (this.currentPage <= 3) {
        this.pageNumbers = [
          1,
          2,
          3,
          '...',
          this.totalPages - 1,
          this.totalPages,
        ];
      } else if (this.currentPage >= this.totalPages - 2) {
        this.pageNumbers = [
          1,
          2,
          '...',
          this.totalPages - 2,
          this.totalPages - 1,
          this.totalPages,
        ];
      } else {
        this.pageNumbers = [
          1,
          2,
          '...',
          this.currentPage - 1,
          this.currentPage,
          this.currentPage + 1,
          '...',
          this.totalPages - 1,
          this.totalPages,
        ];
      }
    }
  }

  isNumber(pageNumber: number | string): boolean {
    return typeof pageNumber === 'number';
  }

  handlePageNumberClick(pageNumber: number | string): void {
    if (this.isNumber(pageNumber)) {
      this.onPageClick(pageNumber as number);
    }
  }

  trackByIndex(index: number, item: number | string): number {
    return index;
  }
}
