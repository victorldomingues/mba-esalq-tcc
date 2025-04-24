import { HttpRequest, HttpHandlerFn, HttpContextToken } from "@angular/common/http";
import { v4 as uuidv4 } from 'uuid';

export function correlationIdInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
    const requisicao = req.clone({
        headers: req.headers.append('Correlation-Id', uuidv4()),
    });
    return next(requisicao);
}