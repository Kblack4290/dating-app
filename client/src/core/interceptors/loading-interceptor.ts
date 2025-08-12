import { HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize, of,tap } from 'rxjs';
import { BusyService } from '../services/busy-service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
const busyService = inject(BusyService);
const cache = new Map<string, HttpEvent<unknown>>();

if(req.method === 'GET'){
  const cachedResponse = cache.get(req.url);
  if(cachedResponse){
    return of(cachedResponse);
  }
}
busyService.busy()

  return next(req).pipe(
    delay(500),
    tap(response => {
      cache.set(req.url, response)
    }),
    finalize(() => {
      busyService.idle()
    })
  )
};
