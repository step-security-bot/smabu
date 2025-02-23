/* tslint:disable */
/* eslint-disable */
/**
 * LIT.Smabu.API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

import { Currency } from './currency';
import { CustomerDTO } from './customer-dto';
import { DatePeriod } from './date-period';
import { InvoiceId } from './invoice-id';
import { InvoiceItemDTO } from './invoice-item-dto';
import { InvoiceNumber } from './invoice-number';
import { TaxRate } from './tax-rate';
 /**
 * 
 *
 * @export
 * @interface InvoiceDTO
 */
export interface InvoiceDTO {

    /**
     * @type {string}
     * @memberof InvoiceDTO
     */
    displayName?: string | null;

    /**
     * @type {InvoiceId}
     * @memberof InvoiceDTO
     */
    id?: InvoiceId;

    /**
     * @type {Date}
     * @memberof InvoiceDTO
     */
    createdAt?: Date | null;

    /**
     * @type {CustomerDTO}
     * @memberof InvoiceDTO
     */
    customer?: CustomerDTO;

    /**
     * @type {InvoiceNumber}
     * @memberof InvoiceDTO
     */
    number?: InvoiceNumber;

    /**
     * @type {string}
     * @memberof InvoiceDTO
     */
    invoiceDate?: string | null;

    /**
     * @type {number}
     * @memberof InvoiceDTO
     */
    amount?: number;

    /**
     * @type {Currency}
     * @memberof InvoiceDTO
     */
    currency?: Currency;

    /**
     * @type {DatePeriod}
     * @memberof InvoiceDTO
     */
    performancePeriod?: DatePeriod;

    /**
     * @type {number}
     * @memberof InvoiceDTO
     */
    fiscalYear?: number;

    /**
     * @type {TaxRate}
     * @memberof InvoiceDTO
     */
    taxRate?: TaxRate;

    /**
     * @type {boolean}
     * @memberof InvoiceDTO
     */
    isReleased?: boolean;

    /**
     * @type {Date}
     * @memberof InvoiceDTO
     */
    releasedAt?: Date | null;

    /**
     * @type {Array<InvoiceItemDTO>}
     * @memberof InvoiceDTO
     */
    items?: Array<InvoiceItemDTO> | null;
}
