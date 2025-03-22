import { HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class GlobalErrorHandlerService {
  private toastr = inject(ToastrService);

  showError(error: any) {
    console.log(error);
    if (error instanceof HttpErrorResponse) {
      const httpError = error as HttpErrorResponse;

      if (httpError.error) {
        if (
          httpError.error.errors.errorMessage &&
          Array.isArray(httpError.error.errors.errorMessage)
        ) {
          for (const err of httpError.error.errors) {
            let detailedMessage = '';
            detailedMessage += `${err.errorMessage}`;
            this.toastr.error(detailedMessage.trim());
          }
        } else if (httpError.error.errors) {
          for (const err of httpError.error.errors) {
            this.toastr.error(err);
          }
        } else {
          this.toastr.error('SERVER ERROR');
        }
      } else {
        this.toastr.error('HTTP ERROR');
      }
    } else {
      this.toastr.error('Unknown error');
    }
  }

  showSuccess(message: string, title?: string, options?: any) {
    this.toastr.success(message, title, options);
  }

  showInfo(message: string, title?: string, options?: any) {
    this.toastr.info(message, title, options);
  }

  showWarning(message: string, title?: string, options?: any) {
    this.toastr.warning(message, title, options);
  }
}
