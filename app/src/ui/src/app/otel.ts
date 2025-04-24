import { EnvironmentProviders, provideAppInitializer } from '@angular/core';
import {
  BatchSpanProcessor,
  ConsoleSpanExporter,
  SimpleSpanProcessor,
} from '@opentelemetry/sdk-trace-base';
import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { OTLPTraceExporter } from '@opentelemetry/exporter-trace-otlp-proto';
import { Resource } from '@opentelemetry/resources';
import { ATTR_SERVICE_NAME, ATTR_SERVICE_VERSION } from '@opentelemetry/semantic-conventions';
import { getWebAutoInstrumentations } from '@opentelemetry/auto-instrumentations-web';
import { environment } from '../environments/environment';

export function provideInstrumentation(): EnvironmentProviders {
  return provideAppInitializer(() => {

    const provider = new WebTracerProvider({
      spanProcessors: [new SimpleSpanProcessor(new ConsoleSpanExporter()), new BatchSpanProcessor(
        new OTLPTraceExporter({
          url: `${window.origin}/v1/traces`
        }),
      )]
    }); // For demo purposes only, immediately log traces to the console


    provider.register({
      contextManager: new ZoneContextManager(),
    }); // Register instrumentations to automatically capture traces from

    registerInstrumentations({
      instrumentations: [
        getWebAutoInstrumentations({
          '@opentelemetry/instrumentation-document-load': {},
          '@opentelemetry/instrumentation-user-interaction': {},
          '@opentelemetry/instrumentation-fetch': {},
          '@opentelemetry/instrumentation-xml-http-request': {},
        }),
      ],
    });
  });
}