import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from './auth.service';
import { LoggerService } from '../logging/logger.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const logger = inject(LoggerService);

  if (authService.getIsAuthenticatedValue()) {
    return true;
  }

  logger.log('Acceso denegado: Intento de acceder a ruta protegida');
  router.navigate(['/home']);
  return false;
};
