import { HttpRequest, HttpHandlerFn, HttpContextToken } from "@angular/common/http";

export const AUTH_INTERCEPTOR_SEM_LOGAR = new HttpContextToken<boolean>(() => false);

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
  if (req.context.get(AUTH_INTERCEPTOR_SEM_LOGAR))
    return next(req);
  const token = localStorage.getItem('accessToken');
  const requisicao = req.clone({
    headers: req.headers.append('Authorization', `Bearer ${token}`),
  });
  return next(requisicao);
}