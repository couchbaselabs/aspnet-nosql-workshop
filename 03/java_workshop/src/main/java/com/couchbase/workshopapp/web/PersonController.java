package com.couchbase.workshopapp.web;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

import com.couchbase.client.deps.io.netty.util.internal.StringUtil;
import com.couchbase.workshopapp.entity.Person;
import com.couchbase.workshopapp.service.PersonService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;
import com.couchbase.workshopapp.entity.Person;

/**
 * Person controller to handle crud http web requests for person repository
 */
@CrossOrigin()
@RestController
public class PersonController {

    private final PersonService personService;

    @Autowired
    public PersonController(PersonService personService) {
        this.personService = personService;
    }

    /**
     * Find all people in the person repository using select n1ql query
	 *
     * Returns an array of people in the repository
     * Returns a failure status, if the n1ql query failed
     */
    @RequestMapping(value = "/api/getAll", method = RequestMethod.GET)
    public ResponseEntity getAll() {

        // TODO: try
            // TODO: use the personService to getAll persons and put them into a PersonCollection
            PersonCollection personList = null;
            // TODO: return the persons in the personList as a 200 (ok) ResponseEntity
            return null;
        // TODO: catch
            // if there's an exception, then put error information into a Map<String, Object> (status, error)
            // and return that response with a 500 (internal server error)
    }

    /**
     * Get a single person entity using id stored as couchbase document using
     * a key value lookup on the key
     *
     * @param id Id of the person entity
     * Returns bad request if the id for the person is missing
     * Returns the person entity if get was successful
     * Returns failure status if the get operation was not successful
     */
    @RequestMapping(value = "/api/get/{id}", method = RequestMethod.GET)
    public ResponseEntity get(@PathVariable("id") String id) {
        Map<String, Object> response = new HashMap<String, Object>();
        if (StringUtil.isNullOrEmpty(id)) {
            response.put("status", "failed");
            response.put("error", "missing person id");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(response);
        }
        try {
            Person person = personService.get(id);
            if (person != null) {
                return ResponseEntity.ok(person);
            } else {
                throw new Exception("Document not found");
            }
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }

    /**
     * Save a person entity
     *
     * @param person json body containing the person's information
     * Returns success if the save operation was successful
     * Returns failure if there was a cas mismatch when using the Version property in person entity
     * or if there are any other errors that caused an operation failure
     */

    @RequestMapping(value = "/api/save", method = RequestMethod.POST)
    public ResponseEntity<Map<String, Object>> save(@RequestBody Person person) {
        Map<String, Object> response = new HashMap<String, Object>();
        if (StringUtil.isNullOrEmpty(person.firstname) || StringUtil.isNullOrEmpty(person.lastname) || StringUtil.isNullOrEmpty(person.email)) {
            response.put("status", "failed");
            response.put("error", "missing person info");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(response);
        }
        try {
            // TODO: if the id field on the person is null or empty, set it to a random UUID
            if (StringUtil.isNullOrEmpty(person.id)) {
                UUID uuid = null; // TODO
                person.id = null; // TODO
            } else {
                // commented out CAS-related code
                //Person existingPerson = personService.get(person.id);
                //person.Version = existingPerson.Version;
            }

            // TODO: use the save operation on personService to save the person

            response.put("status", "ok");
            return ResponseEntity.ok(response);
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }


    /**
     * Delete a person entity using json containing id property
     *
     * @param req unique identifier for the person {"id":"1"}
     * Return bad request if the id property is missing
     * Returns success if the delete succeeded
     * Return failure if there was a delete operation failure
     */
    @RequestMapping(value = "/api/delete", method = RequestMethod.POST)
    public ResponseEntity<Map<String, Object>> delete(@RequestBody Map<String, String> req) {
        Map<String, Object> response = new HashMap<String, Object>();
        String id = req.get("id");
        if (StringUtil.isNullOrEmpty(id)) {
            response.put("status", "failed");
            response.put("error", "missing id");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(response);
        }
        try {
            // TODO: use the delete method on personService to delete a document by its id

            response.put("status", "ok");
            return ResponseEntity.ok(response);
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }
}