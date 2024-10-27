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

import { InvoiceIdOrderReferenceDTO } from './invoice-id-order-reference-dto';
import { OfferIdOrderReferenceDTO } from './offer-id-order-reference-dto';
 /**
 * 
 *
 * @export
 * @interface GetReferencesReadModel
 */
export interface GetReferencesReadModel {

    /**
     * @type {Array<OfferIdOrderReferenceDTO>}
     * @memberof GetReferencesReadModel
     */
    offers?: Array<OfferIdOrderReferenceDTO> | null;

    /**
     * @type {Array<InvoiceIdOrderReferenceDTO>}
     * @memberof GetReferencesReadModel
     */
    invoices?: Array<InvoiceIdOrderReferenceDTO> | null;

    /**
     * @type {number}
     * @memberof GetReferencesReadModel
     */
    offerAmount?: number;

    /**
     * @type {number}
     * @memberof GetReferencesReadModel
     */
    invoiceAmount?: number;
}
